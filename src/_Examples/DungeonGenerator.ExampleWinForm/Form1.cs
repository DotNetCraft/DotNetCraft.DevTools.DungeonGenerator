using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.Utils;

namespace DungeonGenerator.ExampleWinForm
{
    public partial class Form1 : Form
    {
        private readonly IRandom _random;
        private readonly IBinarySpacePartitioningBuilder _binarySpacePartitioningBuilder;
        private readonly IGraphBuilder _graphBuilder;
        private readonly IGraphAlgorithms _graphAlgorithms;
        private Graphics _drawPanelGraphics;

        private GraphDoc _graph;
        private List<Leaf> _leaves;
        private GraphDoc _mstGraph = null;

        public Form1(IRandom random, IBinarySpacePartitioningBuilder binarySpacePartitioningBuilder, IGraphBuilder graphBuilder, IGraphAlgorithms graphAlgorithms)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _binarySpacePartitioningBuilder = binarySpacePartitioningBuilder ?? throw new ArgumentNullException(nameof(binarySpacePartitioningBuilder));
            _graphBuilder = graphBuilder ?? throw new ArgumentNullException(nameof(graphBuilder));
            _graphAlgorithms = graphAlgorithms ?? throw new ArgumentNullException(nameof(graphAlgorithms));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var width = int.Parse(textBoxWidth.Text);
            var height = int.Parse(textBoxHeight.Text);

            var mainRect = new Rect(0, 0, width, height);
            var buildConfig = new BuildConfig
            {
                MinSize = int.Parse(textBoxMinSize.Text),
                MaxSize = int.Parse(textBoxMaxSize.Text),
                SkipRoomFunc = (rect, leaves) =>
                {
                    var p = _random.RandomNumber(0, 101);
                    return p < 50;
                }
            };

            _leaves = _binarySpacePartitioningBuilder.Build(mainRect, buildConfig);
            _graph = _graphBuilder.Build(_leaves);

            var rootVertex = int.Parse(textBoxRootVertex.Text);
            _mstGraph = new GraphDoc
            {
                Graph = _graphAlgorithms.Kruskals_MST(_graph.Graph, rootVertex),
                Metadata = _graph.Metadata
            };

            RefreshScreen();
        }

        private void RefreshScreen()
        {
            var scale = int.Parse(textBoxScale.Text);
            var width = int.Parse(textBoxWidth.Text);
            var height = int.Parse(textBoxHeight.Text);

            _drawPanelGraphics.Clear(Color.White);
            _drawPanelGraphics.DrawRectangle(new Pen(Color.Black), 0, 0, scale * width, scale * height);

            if (_leaves != null && checkBox1.Checked)
            {
                var leafPen = new Pen(Color.Blue);
                foreach (var leaf in _leaves)
                {
                    if (leaf.ActiveLeaf)
                        _drawPanelGraphics.DrawRectangle(leafPen, scale * leaf.Bounds.X, scale * leaf.Bounds.Y,
                            scale * leaf.Bounds.Width, scale * leaf.Bounds.Height);
                }
            }

            if (_graph != null && checkBox2.Checked)
            {
                var edgePen = new Pen(Color.Green);
                var vertexPen = new Pen(Color.Green);
                DrawGraph(_graph, vertexPen, scale, edgePen);
            }

            if (_mstGraph != null && checkBox3.Checked)
            {
                var edgePen = new Pen(Color.Green, 3);
                var vertexPen = new Pen(Color.Green);
                DrawGraph(_mstGraph, vertexPen, scale, edgePen);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var rootVertex = int.Parse(textBoxRootVertex.Text);
            _mstGraph.Graph = _graphAlgorithms.Kruskals_MST(_graph.Graph, rootVertex);
            _mstGraph.Metadata = _graph.Metadata;

            RefreshScreen();
        }

        public void DrawString(float x, float y, string drawString, Graphics formGraphics)
        {
            using (Font drawFont = new Font("Arial", 12))
            using (SolidBrush drawBrush = new SolidBrush(Color.Black))
            {
                StringFormat drawFormat = new StringFormat();
                formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }

        private void DrawGraph(GraphDoc graph, Pen vertexPen, int scale, Pen edgePen)
        {
            var rootVertex = int.Parse(textBoxRootVertex.Text);
            var invalidPen = new Pen(Color.Red, 2);
            var rootPen = new Pen(Color.Red, 4);
            foreach (var vertex in graph.Graph.Vertices)
            {
                var rect1 = graph.Metadata.Geometries[vertex];
                var c1 = rect1.Center * scale;
                if (vertex == rootVertex)
                    _drawPanelGraphics.DrawEllipse(rootPen, (int)(c1.X - 2 - scale / 2.0), (int)(c1.Y - 2 - scale / 2.0), 2 + scale, 2 + scale);
                else
                    _drawPanelGraphics.DrawEllipse(vertexPen, (int)(c1.X - scale / 2.0), (int)(c1.Y - scale / 2.0), scale, scale);
                DrawString(c1.X, c1.Y, vertex.ToString(), _drawPanelGraphics);

                var vertex1 = vertex;
                var edges = graph.Graph.Edges.Where(x => x.Vertex1 == vertex1);
                foreach (var edge in edges)
                {
                    var rect2 = graph.Metadata.Geometries[edge.Vertex2];
                    var c2 = rect2.Center * scale;
                    //if (edge.Weight >= 1)
                    _drawPanelGraphics.DrawLine(edgePen, c1.X, c1.Y, c2.X, c2.Y);
                    //else
                    //  _drawPanelGraphics.DrawLine(invalidPen, c1.X, c1.Y, c2.X, c2.Y);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _drawPanelGraphics = drawPanel.CreateGraphics();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RefreshScreen();
        }
    }
}

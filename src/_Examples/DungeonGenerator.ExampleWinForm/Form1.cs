using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry.Vectors;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;

namespace DungeonGenerator.ExampleWinForm
{
    public partial class Form1 : Form
    {
        private readonly IBinarySpacePartitioningBuilder _binarySpacePartitioningBuilder;
        private readonly IGraphBuilder _graphBuilder;
        private readonly IGraphAlgorithms _graphAlgorithms;
        private Graphics _drawPanelGraphics;

        private Graph _graph;

        public Form1(IBinarySpacePartitioningBuilder binarySpacePartitioningBuilder, IGraphBuilder graphBuilder, IGraphAlgorithms graphAlgorithms)
        {
            _binarySpacePartitioningBuilder = binarySpacePartitioningBuilder ?? throw new ArgumentNullException(nameof(binarySpacePartitioningBuilder));
            _graphBuilder = graphBuilder ?? throw new ArgumentNullException(nameof(graphBuilder));
            _graphAlgorithms = graphAlgorithms ?? throw new ArgumentNullException(nameof(graphAlgorithms));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var scale = int.Parse(textBoxScale.Text);
            var width = int.Parse(textBoxWidth.Text);
            var height = int.Parse(textBoxHeight.Text);

            var mainRect = new Rect(0, 0, width, height);
            var buildConfig = new BuildConfig
            {
                MinSize = int.Parse(textBoxMinSize.Text),
                MaxSize = int.Parse(textBoxMaxSize.Text)
            };

            var leaves = _binarySpacePartitioningBuilder.Build(mainRect, buildConfig);

            _graph = _graphBuilder.Build(leaves);
            var mstGraph = _graphAlgorithms.Kruskals_MST(_graph);
            
            _drawPanelGraphics.Clear(Color.White);
            _drawPanelGraphics.DrawRectangle(new Pen(Color.Black), 0, 0, scale * width, scale * height);

            var leafPen = new Pen(Color.Blue);
            foreach (var leaf in leaves)
            {
                if (leaf.ActiveLeaf)
                    _drawPanelGraphics.DrawRectangle(leafPen, scale * leaf.Bounds.X, scale * leaf.Bounds.Y, scale * leaf.Bounds.Width, scale * leaf.Bounds.Height);
            }

            var centers = leaves.Where(x => x.ActiveLeaf).Select(x => x.Bounds.Center * scale).ToList();
            
            var edgePen = new Pen(Color.Red);
            var vertexPen = new Pen(Color.Red);
            DrawGraph(_graph, centers, vertexPen, scale, edgePen);

            edgePen = new Pen(Color.Green, 2);
            vertexPen = new Pen(Color.Green);
            DrawGraph(mstGraph, centers, vertexPen, scale, edgePen);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void DrawGraph(Graph graph, List<Vector2> centers, Pen vertexPen, int scale, Pen edgePen)
        {
            foreach (var vertex in graph.Vertices)
            {
                var edges = graph.Edges.Where(x => x.Vertex1 == vertex);
                foreach (var edge in edges)
                {
                    var c1 = centers[edge.Vertex1];
                    var c2 = centers[edge.Vertex2];

                    _drawPanelGraphics.DrawEllipse(vertexPen, (int)(c1.X - scale / 2.0), (int)(c1.Y - scale / 2.0), scale,
                        scale);
                    _drawPanelGraphics.DrawEllipse(vertexPen, (int)(c2.X - scale / 2.0), (int)(c2.Y - scale / 2.0), scale,
                        scale);

                    _drawPanelGraphics.DrawLine(edgePen, c1.X, c1.Y, c2.X, c2.Y);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _drawPanelGraphics = drawPanel.CreateGraphics();
        }
    }
}

// Вставьте сюда финальное содержимое файла DotGraphBuilder.cs
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FluentApi.Graph{

    public class DotGraphBuilder{
        
        internal Graph GGRAPH;
        
        internal string NODE_EDGE;
      
        internal GraphNode cur_NODE;
        internal GraphEdge cur_EDGE;
//=============================================================================================
        public static DotGraphBuilder DirectedGraph(string graphName){
            return new DotGraphBuilder() { GGRAPH = new Graph(graphName, true, true) };
        }
//=============================================================================================
        public static DotGraphBuilder NondirectedGraph(string graphName){
            return new DotGraphBuilder() { GGRAPH = new Graph(graphName, false, false) };
        }
    }
//=============================================================================================
    public static class DotGraphBuilderExtention{
        
        public static string Build(this DotGraphBuilder graphBuilder){
            
            return graphBuilder.GGRAPH.ToDotFormat();
        }

        public static DotGraphBuilder AddNode(this DotGraphBuilder graphBuilder, string NODE_NAME){
            
            graphBuilder.GGRAPH.AddNode(NODE_NAME);
            foreach (var node in graphBuilder.GGRAPH.Nodes){
                if (node.Name == NODE_NAME) {graphBuilder.cur_NODE = node;}
            }
            graphBuilder.NODE_EDGE = "node";
            return graphBuilder;
        }
//=============================================================================================
        public static DotGraphBuilder AddEdge(this DotGraphBuilder graphBuilder, string NODE_BEG, string NODE_END){
            
            graphBuilder.GGRAPH.AddEdge(NODE_BEG, NODE_END);
            graphBuilder.NODE_EDGE = "edge";
            foreach (var edge in graphBuilder.GGRAPH.Edges)
                if (edge.SourceNode == NODE_BEG && edge.DestinationNode == NODE_END) {graphBuilder.cur_EDGE = edge;}
            return graphBuilder;
        }
//=============================================================================================
        public static DotGraphBuilder With(this DotGraphBuilder graphBuilder, Action<object> action){
            
            if (graphBuilder.NODE_EDGE == "node") {action(graphBuilder.cur_NODE);}
            
            else if (graphBuilder.NODE_EDGE == "edge") {action(graphBuilder.cur_EDGE);}
            
            return graphBuilder;
        }
    }
//=============================================================================================
    public static class NodeEdgeExtention
    {
        public static object Color(this object item, string Ccolor){
            
            if (item as GraphNode != null) {(item as GraphNode).Attributes.Add("color", Ccolor);}
            
            else if (item as GraphEdge != null) {(item as GraphEdge).Attributes.Add("color", Ccolor);}
            
            return item;
        }
//=============================================================================================
        public static object FontSize(this object item, int Ffont_size){
            
            if (item as GraphNode != null) {(item as GraphNode).Attributes.Add("fontsize", Ffont_size.ToString());}
            
            else if (item as GraphEdge != null) {(item as GraphEdge).Attributes.Add("fontsize", Ffont_size.ToString());}
            
            return item;
        }
//=============================================================================================
        public static object Label(this object item, string Llabel){
            
            if (item as GraphNode != null) {(item as GraphNode).Attributes.Add("label", Llabel);}
            
            else if (item as GraphEdge != null) {(item as GraphEdge).Attributes.Add("label", Llabel);}
            
            return item;
        }
//=============================================================================================
        public static object Weight(this object item, int Wweight){
            
            if (item as GraphNode != null) {(item as GraphNode).Attributes.Add("weight", Wweight.ToString());}
            
            else if (item as GraphEdge != null) {(item as GraphEdge).Attributes.Add("weight", Wweight.ToString());}
            
            return item;
        }
//=============================================================================================
        public static object Shape(this object item, string Sshape){
            
            if (item as GraphNode != null) {(item as GraphNode).Attributes.Add("shape", Sshape);}
            
            return item;
        }
    }
//=============================================================================================
    public class NodeShape{
        
        
        public static string Ellipse = "ellipse";
        
        public static string Box = "box";
        
        
        
        
    }
}
//=============================================================================================

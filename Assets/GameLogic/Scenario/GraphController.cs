using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using G_GraphLib;

public class GraphController : MonoBehaviour {
    // Граф
    G_Graph g_Graph;
    G_Graph karkas; // минимальное остовное дерево
    Dictionary<string, int> trueAnswer; // правильный ответ на задание

    // Настройки круга, на котором будут располагаться узлы
    public float radius = 11.0f;
    float rotation = 0;

    // Узел - игровой объект, который будет копирован
    public GameObject nodeAsset;
    // Ребро
    public GameObject edgeAsset;

    // Структуры данных для узлов, ребер
    // Хранение узлов
    List<GameObject> graphNodes;
    // Соответствие между узлами в графе и на сцене
    Dictionary<Node, GameObject> gr2scene;
    Dictionary<GameObject, Node> scene2gr;
    // Соответствие между ребрами в графе и на сцене
    Dictionary<EdgeTo, GameObject> e_gr2scene;
    Dictionary<GameObject, EdgeTo> e_scene2gr;
    Dictionary<GameObject, Node> e_edgeFrom;

    // Use this for initialization
    void Awake ()
    {
        // input_graph_orient_acycle
        // input_graph_Grigoriev2
        g_Graph = new G_Graph("Assets/GameLogic/Scenario/input_graph_Grigoriev2.txt");
        graphNodes = new List<GameObject>();
        gr2scene = new Dictionary<Node, GameObject>();
        scene2gr = new Dictionary<GameObject, Node>();

        e_gr2scene = new Dictionary<EdgeTo, GameObject>();
        e_scene2gr = new Dictionary<GameObject, EdgeTo>();
        e_edgeFrom = new Dictionary<GameObject, Node>();
        //print(g_Graph.GetGraph().Keys.Count);
    }

    // Создает узлы на сцене а также соединяет граф на сцене с логическим
    void MakeNodes()
    {
        rotation = 0;
        foreach (var node in g_Graph.GetGraph().Keys)
        {
            var createdNode = 
                GameObject.Instantiate(nodeAsset, 
                this.transform.position + Vector3.forward * radius, 
                transform.rotation);
            createdNode.transform.RotateAround(transform.position, new Vector3(0, transform.position.y), rotation);
            //createdNode.transform.Rotate(new Vector3(0, transform.position.y), rotation);
            rotation += 360 / (g_Graph.GetGraph().Count);
            graphNodes.Add(createdNode);
            gr2scene.Add(node, createdNode);
            scene2gr.Add(createdNode, node);
        }
    }

    // Создает ребра на сцене
    void MakeEdges()
    {
        var graph = g_Graph.GetGraph();
        foreach (var node in graph.Keys)
        {
            foreach (var edge in graph[node])
            {
                var createdEdge =
                    GameObject.Instantiate(edgeAsset,
                    (gr2scene[node].transform.position + gr2scene[edge.GetNodeTo()].transform.position) / 2.0f,
                    transform.rotation, this.transform);
                // Чтобы трансформации не действовали на текст
                var link = createdEdge.GetComponent<LinkToModel>();
                var edgeModel = link.model;

                // Увеличим в размере
                edgeModel.transform.localScale =
                    edgeModel.transform.localScale + new Vector3(0, 1) *
                        ((gr2scene[node].transform.position -
                        gr2scene[edge.GetNodeTo()].transform.position).magnitude) / 2.0f;

                // Положить на бок
                edgeModel.transform.Rotate(new Vector3(1, 0), 90);

                // Повернуть к другому узлу
                var rotationBetweenNodes =
                    Vector3.Angle(createdEdge.transform.position - gameObject.transform.position, 
                    gr2scene[edge.GetNodeTo()].transform.position - gr2scene[node].transform.position);
                var additionalRotation =
                    Vector3.Angle(graphNodes[0].transform.position - gameObject.transform.position,
                    createdEdge.transform.position - gameObject.transform.position);

                // Случай когда реальный угол больше 180 градусов
                additionalRotation = createdEdge.transform.position.x < graphNodes[0].transform.position.x ?
                    -additionalRotation : additionalRotation;
                //print(rotationBetweenNodes);
                //print(additionalRotation);
                edgeModel.transform.Rotate(new Vector3(0, 1), rotationBetweenNodes, Space.World);
                edgeModel.transform.Rotate(new Vector3(0, 1), additionalRotation, Space.World);

                // Указать вес ребра
                TextMesh text = createdEdge.GetComponentInChildren<TextMesh>();
                text.text = edge.GetWeight().ToString();
                
                e_gr2scene.Add(edge, createdEdge);
                e_scene2gr.Add(createdEdge, edge);
                e_edgeFrom.Add(createdEdge, node);
            }
        }
    }

    public void RemoveEdge(GameObject edge)
    {
        g_Graph.DeleteEdgeFromTo(e_edgeFrom[edge], e_scene2gr[edge].GetNodeTo());
        e_scene2gr.Remove(edge);
        e_edgeFrom.Remove(edge);
        e_scene2gr.Remove(edge);
        GameObject.Destroy(edge);
    }

    public bool CheckGraph()
    {
        var k_graph = g_Graph.GetGraph();
        var occurs = new Dictionary<string, int>();
        // Инициализация подсчета вершин
        foreach (var node in k_graph.Keys)
        {
            occurs.Add(node.GetName(), 0);
        }
        foreach (var node in k_graph.Keys)
        {
            foreach (var edge in k_graph[node])
            {
                occurs[node.GetName()]++;
                occurs[edge.GetNodeTo().GetName()]++;
            }
        }

        // А ТЕПЕРЬ ПРОВЕРКА, СОВПАДАЕТ ЛИ
        if (occurs.Keys.Count == 0) return false;
        foreach (var node in occurs.Keys)
        {
            if (trueAnswer[node] != occurs[node]) return false;
        }

        return true;
    }

    void InitKarkas()
    {
        karkas = G_Graph.Boruv(g_Graph);
        // Костыльно но сойдет
        var k_graph = karkas.GetGraph();
        var occurs = new Dictionary<string, int>();
        // Инициализация подсчета вершин
        foreach (var node in k_graph.Keys)
        {
            occurs.Add(node.GetName(), 0);
        }
        foreach (var node in k_graph.Keys)
        {
            foreach (var edge in k_graph[node])
            {
                occurs[node.GetName()]++;
                occurs[edge.GetNodeTo().GetName()]++;
            }
        }
        trueAnswer = occurs;
    }

	void Start () {
        //print(g_Graph.GetGraph().Keys.Count);
        MakeNodes();
        MakeEdges();
        InitKarkas();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

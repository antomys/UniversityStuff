import edu.princeton.cs.algs4.Graph;
import edu.princeton.cs.algs4.BreadthFirstPaths;

//n=20, m=26, k=3, d=7, t1=11, t2=9, t3=15
class GraphSolver
{
	public static void main (String [] argv)
	{
		Graph G = new Graph (20);
		// component 1
		for (int i = 0; i < 8; i++)
			G.addEdge (i, i+1);
		G.addEdge (5, 7);
		G.addEdge (6, 8);
		// component 2
		for (int i = 9; i < 15; i++)
		{
			G.addEdge (i, i+1);
		}
		G.addEdge (9, 15);
		G.addEdge (10, 12);
		G.addEdge (11, 14);
		G.addEdge (11, 15);
		G.addEdge (13, 15);
		//component 3
		for (int i = 16; i < 19; i++)
		{
			G.addEdge (i, i+1);
		}
		G.addEdge (17, 19);
		G.addEdge (16, 19);
		/*
		for (int i = 0; i < 20; i++)
			for (int edge : G.adj(i))
				System.out.println ("Edge " + i + "-" + edge);
		* */
		
		BreadthFirstPaths[] bfsArray = new BreadthFirstPaths [20];
		int[] maxLength = new int [20];
		
		for (int i = 0; i < 20; i++)
		{
			bfsArray [i] = new BreadthFirstPaths (G, i);
			maxLength[i] = -1;
			for (int j = 0; j < 20; j++)
			{
				if (bfsArray[i].distTo(j) > maxLength[i] && i != j && bfsArray[i].hasPathTo (j))
				{
					maxLength[i] = bfsArray[i].distTo(j);
				}
			}
		}
		
		

		
		for (int i = 0; i < 20; i++)
		{
			System.out.println ("Vertex " + (i+1) + " has a degree of " + G.degree (i));
			System.out.println ("Vertex " + (i+1) + " has an extricitent of " + maxLength[i]);
			System.out.println();
			System.out.println();
		}
	}	
}

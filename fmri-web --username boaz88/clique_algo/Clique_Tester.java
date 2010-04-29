package clique_algo;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Vector;

public class Clique_Tester {
	public static int Q_size = 9;
	public static double TH = 0.7;
	public static String in_file = "test1.csv";
	
	
	public static void main(String[] args) {
		Graph G = new Graph(in_file, TH);
		Vector<VertexSet> c7 = G.All_Cliques(7);
		printAll(c7);
		write2file(c7);
	}
	
	
	static void printAll(Vector<VertexSet> c) {
		for(int i=0;i<c.size();i++) {
			System.out.println(i+") "+c.elementAt(i));
		}
	}
	
	
	static void write2file(Vector<VertexSet> c) {
		FileWriter fw=null;
		try {fw = new FileWriter("ALL_Cliques_"+in_file+"_"+TH+"_"+Q_size+".txt");} 
		catch (IOException e) {e.printStackTrace();}
		PrintWriter os = new PrintWriter(fw);
		os.println("ALL_Cliques: of file: "+in_file+",  TH:"+TH+" Max Q:"+Q_size);
		for(int i=0;i<c.size();i++) {
			os.println(i+") "+c.elementAt(i));
		}
		os.close();
		try {
			fw.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
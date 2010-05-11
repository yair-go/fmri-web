package clique_algo;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Vector;

public class Clique_Tester {
	public static int minQ = 5, maxQ=7;
	public static double TH = 0.8;
	public static String in_file = "test1.csv";
	public static String out_file = in_file+"_out.txt";
	
	
	public static void main(String[] args) {
		if(args==null || args.length<4) {
			help();
		}
		else {
			parse(args);
			Graph G = new Graph(in_file, TH);
			//Vector<VertexSet> c5_7 = G.All_Cliques(minQ,maxQ);
			Vector<VertexSet> c5_7 = G.All_Cliques(maxQ);
			printAll(c5_7);
			write2file(c5_7);
		}
	}
	static void help() {
		System.out.println("Wrong Parameters! should use: java -jar All_Cliques <input file> <round value> <min clique> <max clique> <output file>");
		System.out.println("Wrong Parameters! should use: java -jar All_Cliques test1.csv 0.7 5 7 test1_out.txt");
	}
	static void parse(String[] a){
		try {
			in_file = a[0];
			out_file = a[4];
			TH = new Double(a[1]);
			minQ=new Integer(a[2]);
			maxQ=new Integer(a[3]);
		}
		catch(Exception e) {
			e.printStackTrace();
			help();
		}
	}
	
	static void printAll(Vector<VertexSet> c) {
		for(int i=0;i<c.size();i++) {
			VertexSet curr = c.elementAt(i);
			if(curr.size()>=minQ) {
				System.out.println(i+") "+curr);
			}
		}
	}
	
	
	static void write2file(Vector<VertexSet> c) {
		FileWriter fw=null;
		try {fw = new FileWriter(out_file);} 
		catch (IOException e) {e.printStackTrace();}
		PrintWriter os = new PrintWriter(fw);
		os.println("ALL_Cliques: of file: "+in_file+",  TH:"+TH+" Max Q:"+maxQ);
		for(int i=0;i<c.size();i++) {
			VertexSet curr = c.elementAt(i);
			if(curr.size()>=minQ) os.println(i+") "+curr);
		}
		os.close();
		try {
			fw.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
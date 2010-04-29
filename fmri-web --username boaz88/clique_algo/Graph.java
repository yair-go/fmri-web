package clique_algo;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.StringTokenizer;
import java.util.Vector;

/**
 * this class represents an undirected 0/1 sparse Graph 
 * @author Boaz
 *
 */
 class Graph {
	 private String _file_name;
	 Vector <VertexSet> _V;
	 double _TH; // the threshold value
	 
	 Graph(String file, double th) {
		this._file_name = file;
		_TH = th;
		_V = new  Vector <VertexSet>();
		 init();
	 }
	 
	private void init() {
		FileReader fr=null;
		try {
			fr = new FileReader(this._file_name);
		} catch (FileNotFoundException e) {	e.printStackTrace();}
		BufferedReader is = new BufferedReader(fr);
		try {
			String s = is.readLine();
			StringTokenizer st = new StringTokenizer(s,", ");
			int len = st.countTokens();
			
			while(s!=null) {
				int line = 0;
				VertexSet vs = new VertexSet();
				for(int i=0;i<len;i++) {
					float v = new Double(st.nextToken()).floatValue();
					if(v>_TH) vs.add(i);
				}
				this._V.add(vs);
				line++;
				s = is.readLine();
			if(s!=null)	st = new StringTokenizer(s,", ");
			}
			
		} catch (IOException e) {e.printStackTrace();}
	 }
	
	public VertexSet Ni(int i) {
		VertexSet ans = _V.elementAt(i);
		return  ans;
	}
	public void print() {
		System.out.println("Graph: |V| = "+this._V.size());
		for(int i=0;i<this._V.size();i++) {
			System.out.println();
			System.out.print("V["+i+"] = ");
			VertexSet curr = this._V.elementAt(i);
			for(int a=0;a<curr.size();a++) 
				System.out.print(curr.at(a)+", ");
		}
	}
	
	/*************** Clique Algorithms ******************/
	Vector<VertexSet>  All_Cliques(int Q_size) {
		Vector<VertexSet> ans = new Vector<VertexSet>();
		Vector<VertexSet>C0 = allEdges(); // all edges – all cliques of size 2/
		ans.addAll(C0);
		for(int i=3;i<=Q_size;i++) {
			Vector<VertexSet>C1 = allC(C0);
			ans.addAll(C1);
			C0 = C1;
		} // for
		return ans;
	}

	Vector<VertexSet> allC(Vector<VertexSet> C0) {
		Vector<VertexSet> ans = new Vector<VertexSet>();
		for(int i=0;i<C0.size();i++) {
			VertexSet curr = C0.elementAt(i);
			VertexSet inter = intersection(curr);
			if(inter.size()>0)  
				addbiggerCliQ(ans,curr,inter); // strange clique expqnding function
	}	
		return ans;	
	}
	VertexSet intersection(VertexSet C) {
		VertexSet ans = _V.elementAt(C.at(0));
		for(int i=0;ans.size()>0 & i<C.size();i++) 
			ans = ans.intersection(_V.elementAt(C.at(i)));
		return ans;
	}
	private void addbiggerCliQ(Vector<VertexSet> ans,VertexSet curr ,VertexSet inter) {
		int last = curr.at(curr.size()-1);
		for(int i=0;i<inter.size();i++) {
			int ind_inter = inter.at(i);
			if(last<ind_inter) {
				VertexSet c = new VertexSet(curr);
				c.add(ind_inter);
				ans.add(c);
			}
		}
	}
	/**
	 * computes all the 2 cliques --> i.e. all the edges 
	 * @return
	 */
	private Vector<VertexSet> allEdges() { // all edges – all cliques of size 2/
		Vector<VertexSet> ans = new Vector<VertexSet>();
		for(int i=0;i<_V.size();i++) {
			VertexSet curr = _V.elementAt(i);
			for(int a=0;a<curr.size();a++) {
				if(i<curr.at(a)) {
					VertexSet tmp = new VertexSet();
					tmp.add(i) ; 
					tmp.add(curr.at(a));
					ans.add(tmp);
				}
			}
			
		}
		return ans;
	}
	//********** TESTS ***********************
}
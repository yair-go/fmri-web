All Cliques v0.1  15/5/2010 

General
The All_Cliques2 jar file allows computing all the cliques of size [min,max] over a graph (represented as matrix).
using iterative method. The code is not bug free and use an edge based DFS like search algorithm over the graph edges 


How to use:
java -jar All_Cliques <input file> <round value> <min clique> <max clique> (optional: <output file> <max cliques>)
java -jar All_Cliques test1.csv 0.77 5 12   // default file name and 100000 cliques
java -jar All_Cliques test1.csv 0.7 5 13 test1_out.txt 300000 



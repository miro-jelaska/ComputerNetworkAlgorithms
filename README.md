# Faculty_ComputerNetworkAlgorithms
Graph ADT and shortest path algorithms + Polynomials ADT and CRC algorithm. Project that I created as part of course on Computer networks on faculty. I wanted to make an useful abstraction of graphs and polynomials so that algorithm implementations would be lightweight (e.g. I don't think that CRC algorithm should know how polynomial division is done. It should only ask for division and Polynomial ADT should take care of the rest.)

It can be devided into two parts:
* Graphs and shortest path algorithms
* Polynomials and CRC algorithm

## Graphs and shortest path algorithms
It comprises of following data structures:
  
:mag_right: | Data structure | Description
--- | --- | ---
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/Distance.cs "Source") | Distance | Simple _struct_ that represents tuple (vertex A, vertex B, edge weight)
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/Edge.cs "Source") | Edge | Makes comparing edges and summing edge weights easier. It also makes having edges with infinit or no weight easier.
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/Graph.cs "Source") | Graph | Represents graph that can be directed or undirected.
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/GraphBuilder.cs "Source") | GraphBuilder | Class inspired by StringBuilder provides handy way to build Graph (which are immutable).
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/GraphWithTracking.cs "Source") | GraphWithTracking | Wraps around Graph and provides a lists to keep track of calculated distances between vertices and to keep track which vertices are visited.

  
  
Algorithm implementations:

:mag_right: | Algorithm
--- | ---
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/DijkstraAlgorithm.cs "Source") | Dijkstra
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/BellmanFordAlgorithm.cs "Source") | BellmanFord

## Polynomials and CRC algorithm
It comprises of following data structures:
  
:mag_right: | Data structure | Description
--- | --- | ---
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Polynomials/DecimalMonomial.cs "Source") | DecimalMonomial | This class represents one part of the Polynomial. This is not an mathematical but artificial structure which is essential building block of Polynomials. In this polynomial _4x^3 + 2x - 1_ monomials are [4x^3, 2x, -1].
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Polynomials/DecimalPolynomial.cs "Source") | DecimalPolynomial | Represents decimal polynomial internaly build out of DecimalMonomials. It makes addition, multiplication, comparison, ... among decimal polynomials easy.
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Polynomials/BinaryPolynomial%20.cs "Source") | BinaryPolynomial | Represents binary polynomial internaly using one _uint_ to hold polynomial. It makes addition, division, comparison, ... among binary polynomials easy.

Algorithm implementations:

:mag_right: | Algorithm
--- | ---
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Polynomials/CrcAlgorithm.cs "Source") | CrcAlgorithm

# Faculty_ComputerNetworkAlgorithms
Project that was created as part of course on Computer networks on faculty.
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

  
  
And it has following algorithm implementations:

:mag_right: | Algorithm
--- | ---
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/DijkstraAlgorithm.cs "Source") | Dijkstra
[<>](https://github.com/MiroslavJelaska/Faculty_ComputerNetworkAlgorithms/blob/master/src/ComputerNetwork/ComputerNetwork.Graphs/BellmanFordAlgorithm.cs "Source") | BellmanFord

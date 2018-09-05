# NET_TO_VISSIM


The goal of this project is to create a stable application based in .NET framework that can connect by COM to PTV VISSIM software in order to take control of its simulation parameters and to enable the use of advanced traffic algorithms as well as the implementation of artificial intelligence.

Project is created as a tool used in the master thesis of Mladen Miletić, Faculty of Transport and Traffic Sciences, University of Zagreb, Croatia.

Project goals:

1. Enable COM communication with PTV VISSIM (v10) software:
  - Launch VISSIM from the application
  - Load a network to VISSIM
  - Manipulate basic simulation parameters
  - Run the simulation(s)
  
2. Gather simulation data:
  - Access all available simulation data after simulation
  - Access all available simulation data during simulation
  - Access all data across multiple simulations
  - Store all gathered data in .csv format
  
3. Implement signal control:
  - Read signal data from each signal controller and group
  - Take control of signal controller to repeat existing signal program
  - Make permament and temporary changes to the signal program
  
4. Implement artificial intelligence:
  - Fuzzy Logic for signal control
  - Genetic algorithm for the optimisation of signal programs
  - Neural network for signal control
  
5. Fluid user interface
  - Use application while VISSIM simulation is running
  - Dynamcally change simulation parameters

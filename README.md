A Simple Genetic Algorithm for Sorting a 13 Card Deck.

This is a simple project intended to showcase genetic algorithms with Sorting 13 Card Deck
Overview
--------
This application simply evolves the integer in a sorted sequence from the random unsorted population. It is intended to be a gentle introduction of genetic algorithm. Using C#, GeneticSharp framework. 

Architecture
============
The genetic algorithm is broken up between two logical units: Chromosomes and a Population. 

Population
==========
Initially we created a random population of size 40. 

Evolution
---------
The evolution algorithm is simple in that it uses TournamentSelection, OrderBasedCrossover and ReverseSequenceMutation.

Tournament Selection
--------------------
This selection involves running several tournaments among a few individual chromosomes chosen at random from the population. The winner of each tournament is selected for crossover.

OrderBasedCrossover
-------------------
This is simple crossover from two parents creating child by swapping the index positions

ReverseSequenceMutation
-----------------------
In the reverse sequence mutation, two randomly generated positions are reversed.

Chromosome
==========
Each chromosome has a gene that represents one possible solutions to the give problem. In our case each gene represents a Card Value that strives to match correct sequence. Each chromosomes have fitness attribute of how close the consecutive genes are there. This measurement is just a simple absolute sum of difference between consecutive card values.
Example:
Target Card Sequence : 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 , 1
Fitness : (13-12) + (12-11) + (11-10) + (10-9) + (9-8) + (8-7) + (7-6) + (6-5) + (5-4) + (4-3) + (3-2) + (2-1) = 12



This code uses the GeneticSharp Framework library https://github.com/giacomelli/GeneticSharp


# Emulator8085
This project is a emulator of the [Intel 8085 CPU](https://en.wikipedia.org/wiki/Intel_8085#Commands/instructions). 

Here are some of its features:
* Data width	8 bits
* Address width	16 bits
* 8 bit registers: A, B, C, D, E, H, L, FLAG, INT (interupt), 
* 16 bit registers: IC (instruction counter), SP (stack pointer)
* 1 and 2 byte instructions 

This project also emulates the [6116 RAM](http://ee-classes.usc.edu/ee459/library/datasheets/6116SA.pdf) with 2048×8 bits memory.

## Motivation

The MC 85/2 helped me a lot to understand how CPU, RAM, BUS, etc. work together and how the programming of a computer works at low level. 

I was curious about how to write an emulator, so my focus was not on the SOLID principles or an user interface - but on the goal to make a functional prototype.

This project is currently not being maintained, because it is a lot of work to implement the whole instruction set.

## The MC 85/2
This project is inspired by the MC 85/2 computer, which uses the Intel 8085 and the 6116 RAM.
The MC 85/2 is a DIY computer. Its purpose is to learn the fundamentals on how a computer works internally. 

The instructions to build and programm this computer are described in the book "Lern-Computer", written by Waldemar Willner and published by "Studienverl. Brockmeyer".

## Current project state

The program that is executed by the CPU is stored in the memory between adress 0x2000 and 0x27FF.

The emulator currently supports following instruction
* MVI A XXX (Move byte XXX to register A)
* MVI B XXX (Move byte XXX to register B)
* MVI C XXX (Move byte XXX to register C)
* MVI D XXX (Move byte XXX to register D)
* MVI E XXX (Move byte XXX to register E)
* MVI H XXX (Move byte XXX to register H)
* MVI L XXX (Move byte XXX to register L)

More instructions on how to run and program the emulator will follow soon!

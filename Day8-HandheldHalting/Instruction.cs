using System;

namespace Day8_HandheldHalting
{
    public class Instruction
    {
        public InstructionType Type { get; set; }
        public int Arg { get; set; }
        
        public int Visited { get; set; }
    }

    public enum InstructionType
    {
        acc = 0,
        jmp,
        nop,
        end
    }
}
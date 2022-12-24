using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

public class LuaTest
{

    public void RunTest(){
        string text = System.IO.File.ReadAllText(@"C:\Users\mclan\Documents\Unity Programs\DndTest\Dnd MVP\Assets\CoreDnD\LuaScripts\test.lua");
	    DynValue res = Script.RunString(text);
        Debug.Log("Lua calc: " + res.Number);
	    // return res.Number;
    }
}

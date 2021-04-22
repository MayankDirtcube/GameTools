using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DC.Tools;

//Attach this Script to a Gameobject and check the inspector
public class DrawIfExample : MonoBehaviour
{
    public enum TestEnum
    {
        show,
        hide
    }

    public TestEnum testEnum;

    [System.Serializable]
    public class TestClass
    {
        public string classString;
        public int classInt;
    }

    [System.Serializable]
    public struct TestStruct
    {
        public string structString;
        public int structInt;
    }
    
    [DrawIf("testEnum", TestEnum.show, DisablingType.DontDraw)]
    public int testInt_Enum;

    [DrawIf("testEnum", TestEnum.show, DisablingType.ReadOnly)]
    public string testStr_Enum;

    public bool testBool;

    [DrawIf("testBool", true)]
    public Vector3 testVector;

    [DrawIf("testBool", true)]
    public TestClass testClass;

    [DrawIf("testBool", true)]
    public TestStruct testStruct;

    //[DrawIf("testBool", true)]
    //public int[] testArray;
    //public List<int> testList;

    [DrawIf("testBool",true)]
    public int xy;
}

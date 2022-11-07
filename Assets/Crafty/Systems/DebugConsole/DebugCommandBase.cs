using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase 
{
    private string _cmdID;
    private string _cmdDescription;
    private string _cmdFormat;

    public string cmdID => _cmdID;
    public string cmdDescription => _cmdDescription;
    public string cmdFormat => _cmdFormat;

    public DebugCommandBase(string id, string description, string format)
    {
        _cmdID = id;
        _cmdDescription = description;
        _cmdFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command;

    public DebugCommand(string id, string description, string format, Action command) : base (id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}

public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> command;

    public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T1 value)
    {
        command.Invoke(value);
    }
}
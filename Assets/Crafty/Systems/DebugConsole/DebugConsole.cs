using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugConsole : MonoBehaviour
{
    string input;
    [SerializeField] bool showingConsole;
    [SerializeField] bool showingHelp;

    public static DebugCommand TEST_COMMAND;
    public static DebugCommand HELP;
    public static DebugCommand<int> INT_COMMAND;

    public List<object> commandList;


    public void OpenConsole(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            showingConsole = !showingConsole;
        }
    }
    public void OnReturn(InputAction.CallbackContext context)
    {
        if (context.performed && showingConsole)
        {
            HandleInput();
            input = "";
        }
    }

    private void Awake()
    {
        TEST_COMMAND = new DebugCommand("test_command", "A test command clearly", "test_command", () =>
         {
             Debug.Log("You ran a test command");
         });
        
        HELP = new DebugCommand("help", "Shows a list of all commands", "help", () =>
         {
             showingHelp = true;
         });

        INT_COMMAND = new DebugCommand<int>("int_command", "Sets the int to input", "int_command <int>", (x) =>
        {
            Debug.Log("you set the int value to: "  + x);
        });



        commandList = new List<object>
        {
            TEST_COMMAND,
            INT_COMMAND,
            HELP,
        };
    }

    Vector2 scroll;

    private void OnGUI()
    {
       if (!showingConsole) { return; }

        float y = 0f;

        if (showingHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.cmdFormat} - {command.cmdDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();
            y += 100;
        }


        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y+5f,Screen.width - 20f, 20f), input);
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');


        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.cmdID))
            {
                if(commandList[i] as DebugCommand != null)
                {
                    Debug.Log("Shouldn't be running");
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }

            }
        }
       
    }
}

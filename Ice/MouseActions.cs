// Copyright (c) Jonathan Thompson 2014

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

using Ice;

public class MouseActions
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out Point lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    /// <summary>
    /// Sets the current cursor position.
    /// </summary>
    /// <param name="X">x-coordinate for cursor position.</param>
    /// <param name="Y">y-coordinate for cursor position.</param>
    public static void SetCursorPosition(int X, int Y)
    {
        SetCursorPos(X, Y);
    }

    /// <summary>
    /// Sets the current cursor position.
    /// </summary>
    /// <param name="point">The point at which to set the cursor.</param>
    public static void SetCursorPosition(Point point)
    {
        SetCursorPos((int)point.X, (int)point.Y);
    }

    /// <summary>
    /// Get the current mouse position.
    /// </summary>
    /// <returns>System.Windows.Point</returns>
    public static Point GetCursorPosition()
    {
        Point currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new Point(0, 0); }
        return currentMousePoint;
    }

    /// <summary>
    /// Click whatever point the cursor's currently at.
    /// </summary>
    public static void Click()
    {
        MouseEvent(MouseEventFlags.LeftDown);
        Thread.Sleep(ScriptProperties.MouseDown);
        MouseEvent(MouseEventFlags.LeftUp);
    }

    /// <summary>
    /// Click a given point base on a Point parameter.
    /// </summary>
    /// <param name="p">Point to click.</param>
    public static void Click(Point p)
    {
        SetCursorPosition(p);
        Click();
    }

    /// <summary>
    /// Click a given point at the provided x and y coordinates.
    /// </summary>
    /// <param name="x">x-coordinate to click.</param>
    /// <param name="y">y-coordinate to click.</param>
    public static void Click(int x, int y)
    {
        SetCursorPosition(x, y);
        Click();
    }

    /// <summary>
    /// Right-click whatever point the cursor's currently at.
    /// </summary>
    public static void RightClick()
    {
        MouseEvent(MouseEventFlags.RightDown);
        Thread.Sleep(ScriptProperties.MouseDown);
        MouseEvent(MouseEventFlags.RightUp);
    }

    /// <summary>
    /// Right-click a given point base on a Point parameter.
    /// </summary>
    /// <param name="p">Point to click.</param>
    public static void RightClick(Point p)
    {
        SetCursorPosition(p);
        RightClick();
    }

    /// <summary>
    /// Right-click a given point at the provided x and y coordinates.
    /// </summary>
    /// <param name="x">x-coordinate to click.</param>
    /// <param name="y">y-coordinate to click.</param>
    public static void RightClick(int x, int y)
    {
        SetCursorPosition(x, y);
        RightClick();
    }

    /// <summary>
    /// Double-click whatever point the cursor's currently at.
    /// </summary>
    public static void DoubleClick()
    {
        Click();
        Thread.Sleep(ScriptProperties.DoubleClickTime);
        Click();
    }

    /// <summary>
    /// Double-click a given point base on a Point parameter.
    /// </summary>
    /// <param name="p">Point to click.</param>
    public static void DoubleClick(Point p)
    {
        SetCursorPosition(p);
        DoubleClick();
    }

    /// <summary>
    /// Double-click a given point at the provided x and y coordinates.
    /// </summary>
    /// <param name="x">x-coordinate to click.</param>
    /// <param name="y">y-coordinate to click.</param>
    public static void DoubleClick(int x, int y)
    {
        SetCursorPosition(x, y);
        DoubleClick();
    }

    /// <summary>
    /// Middle-click whatever point the cursor's currently at.
    /// </summary>
    public static void MiddleClick()
    {
        MouseEvent(MouseEventFlags.MiddleDown);
        Thread.Sleep(ScriptProperties.MouseDown);
        MouseEvent(MouseEventFlags.MiddleUp);
    }

    /// <summary>
    /// Middle-click a given point base on a Point parameter.
    /// </summary>
    /// <param name="p">Point to click.</param>
    public static void MiddleClick(Point p)
    {
        SetCursorPosition(p);
        MiddleClick();
    }

    /// <summary>
    /// Middle-click a given point at the provided x and y coordinates.
    /// </summary>
    /// <param name="x">x-coordinate to click.</param>
    /// <param name="y">y-coordinate to click.</param>
    public static void MiddleClick(int x, int y)
    {
        SetCursorPosition(x, y);
        MiddleClick();
    }


    /// <summary>
    /// Initiates a mouse event.
    /// </summary>
    /// <param name="value">Dictates which mouse action to take.</param>
    private static void MouseEvent(MouseEventFlags value)
    {
        Point position = GetCursorPosition();

        mouse_event
            ((int)value,
             (int)position.X,
             (int)position.Y,
             0,
             0)
            ;
    }
}
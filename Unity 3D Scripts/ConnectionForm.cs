using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using UnityEngine;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Microsoft.Win32;

public class ConnectionForm : MonoBehaviour {
    public static string MA { get; set; }
    public static string MB { get; set; }
    public static int chosenvalueAI1 { get; set; }
    public static int chosenvalueBI1 { get; set; }
    public static int chosenvalueAI2 { get; set; }
    public static int chosenvalueBI2 { get; set; }
    public static byte[] PotentialMotorA = new byte[5];
    public static byte[] IntegralMotorA = new byte[5];
    public static byte[] DerivativeMotorA = new byte[5];
    public static byte[] SmoothingMotorA = new byte[5];
    public static byte[] PotentialMotorB = new byte[5];
    public static byte[] IntegralMotorB = new byte[5];
    public static byte[] DerivativeMotorB = new byte[5];
    public static byte[] SmoothingMotorB = new byte[5];
    public static byte[] PWMMotorA = new byte[5];
    public static byte[] PWMMotorB = new byte[5];
    public static bool ActiveA { get; set; }
    public static bool ActiveB { get; set; }
    public static SerialPort serialPort1 { get; set; }


    System.Windows.Forms.Button button1;
    System.Windows.Forms.ComboBox portList;
    System.Windows.Forms.ComboBox motorASettings;
    System.Windows.Forms.ComboBox motorBSettings;
    System.Windows.Forms.Label label1;
    System.Windows.Forms.Label label2;
    System.Windows.Forms.Label label3;
    System.Windows.Forms.Label label4;
    System.Windows.Forms.TextBox ABox;
    System.Windows.Forms.TextBox BBox;
    System.Windows.Forms.TextBox APWMBox;
    System.Windows.Forms.TextBox BPWMBox;
    System.Windows.Forms.Button button2;
    System.Windows.Forms.Button button3;
    System.Windows.Forms.Button button4;

    Form Form1;
    // Use this for initialization
    void Start() {
        UnityEngine.Application.runInBackground = true;
        OpenFormWindow();
    }

    private void OpenFormWindow()
    {
        serialPort1 = new SerialPort();
        string[] ports = SerialPort.GetPortNames();
        PotentialMotorA[0] = 0x5B;
        PotentialMotorA[1] = 0x44;
        PotentialMotorA[4] = 0x5D;
        IntegralMotorA[0] = 0x5B;
        IntegralMotorA[1] = 0x47;
        IntegralMotorA[4] = 0x5D;
        DerivativeMotorA[0] = 0x5B;
        DerivativeMotorA[1] = 0x4A;
        DerivativeMotorA[4] = 0x5D;
        SmoothingMotorA[0] = 0x5B;
        SmoothingMotorA[1] = 0x4D;
        SmoothingMotorA[4] = 0x5D;
        PWMMotorA[0] = 0x5B;
        PWMMotorA[1] = 0x50;
        PWMMotorA[4] = 0x5D;
        PotentialMotorB[0] = 0x5B;
        PotentialMotorB[1] = 0x45;
        PotentialMotorB[4] = 0x5D;
        IntegralMotorB[0] = 0x5B;
        IntegralMotorB[1] = 0x48;
        IntegralMotorB[4] = 0x5D;
        DerivativeMotorB[0] = 0x5B;
        DerivativeMotorB[1] = 0x4B;
        DerivativeMotorB[4] = 0x5D;
        SmoothingMotorB[0] = 0x5B;
        SmoothingMotorB[1] = 0x4E;
        SmoothingMotorB[4] = 0x5D;
        PWMMotorB[0] = 0x5B;
        PWMMotorB[1] = 0x51;
        PWMMotorB[4] = 0x5D;
        Form1 = new Form();
        Form1.Text = "Platform Settings";
        Form1.Width = 300;
        serialPort1.DtrEnable = true;
        serialPort1.BaudRate = 500000;
        button1 = new System.Windows.Forms.Button();
        button1.Name = "OpenButton";
        button1.Text = "Open";
        button1.Location = new Point(150, 10);
        button1.Width = 80;
        button1.Click += Button1_Click;
        button1.Enabled = true;
        button2 = new System.Windows.Forms.Button();
        button2.Name = "AButton";
        button2.Text = "Submit Value";
        button2.Location = new Point(50, 170);
        button2.Width = 80;
        button2.Click += Button2_Click;
        button2.Enabled = false;
        button3 = new System.Windows.Forms.Button();
        button3.Name = "BButton";
        button3.Text = "Submit Value";
        button3.Location = new Point(140, 170);
        button3.Width = 80;
        button3.Click += Button3_Click;
        button3.Enabled = false;
        button4 = new System.Windows.Forms.Button();
        button4.Name = "CloseButton";
        button4.Text = "Close";
        button4.Location = new Point(150, 50);
        button4.Width = 80;
        button4.Click += Button4_Click;
        button4.Enabled = false;
        Form1.Controls.Add(button1);
        portList = new System.Windows.Forms.ComboBox();
        portList.Name = "portList";
        portList.Location = new Point(30, 30);
        portList.Width = 110;
        if (ports.Length == 1)
        {
            portList.Items.AddRange(new object[]
       {"", ports[0]});
        }
        if (ports.Length == 2)
        {
            portList.Items.AddRange(new object[]
       {"", ports[0], ports[1]});
        }
        if (ports.Length == 3)
        {
            portList.Items.AddRange(new object[]
       {"", ports[0], ports[1], ports[2]});
        }
        if (ports.Length == 4)
        {
            portList.Items.AddRange(new object[]
       {"", ports[0], ports[1], ports[2], ports[3]});
        }
        if (ports.Length == 5)
        {
            portList.Items.AddRange(new object[]
       {"", ports[0], ports[1], ports[2], ports[3], ports[4]});
        }
        label1 = new System.Windows.Forms.Label();
        label1.Text = "Open Serial Port";
        label1.Location = new Point(30, 10);
        label2 = new System.Windows.Forms.Label();
        label2.Text = "Port:";
        label2.Location = new Point(20, 30);
        label3 = new System.Windows.Forms.Label();
        label3.Text = "Motor A Settings";
        label3.Location = new Point(40, 85);
        label4 = new System.Windows.Forms.Label();
        label4.Text = "Motor B Settings";
        label4.Location = new Point(170, 85);
        ABox = new System.Windows.Forms.TextBox();
        ABox.Name = "ABox";
        ABox.Location = new Point(50, 130);
        ABox.Width = 80;
        BBox = new System.Windows.Forms.TextBox();
        BBox.Name = "BBox";
        BBox.Location = new Point(140, 130);
        BBox.Width = 80;
        ABox.Visible = false;
        BBox.Visible = false;
        APWMBox = new System.Windows.Forms.TextBox();
        APWMBox.Name = "APWMBox";
        APWMBox.Location = new Point(50, 150);
        APWMBox.Width = 80;
        APWMBox.Visible = false;
        BPWMBox = new System.Windows.Forms.TextBox();
        BPWMBox.Name = "BPWMBox";
        BPWMBox.Location = new Point(140, 150);
        BPWMBox.Width = 80;
        BPWMBox.Visible = false;
        button2.Visible = false;
        button3.Visible = false;
        motorASettings = new System.Windows.Forms.ComboBox();
        motorASettings.Name = "motorASettings";
        motorASettings.Location = new Point(30, 110);
        motorASettings.Width = 110;
        motorASettings.Items.AddRange(new object[]
        {"",
        "Potential Term",
        "Integral Term",
        "Derivative Term",
        "PWM"
        });
        motorASettings.SelectedIndexChanged += motorASettings_SelectedIndexChanged;
        motorASettings.Enabled = false;
        motorBSettings = new System.Windows.Forms.ComboBox();
        motorBSettings.Name = "motorBSettings";
        motorBSettings.Location = new Point(160, 110);
        motorBSettings.Width = 110;
        motorBSettings.Items.AddRange(new object[]
        {"",
        "Potential Term",
        "Integral Term",
        "Derivative Term",
        "PWM"
        });
        motorBSettings.SelectedIndexChanged += motorBSettings_SelectedIndexChanged;
        motorBSettings.Enabled = false;
        Form1.Controls.Add(button1);
        Form1.Controls.Add(portList);
        Form1.Controls.Add(label1);
        Form1.Controls.Add(label1);
        Form1.Controls.Add(label3);
        Form1.Controls.Add(label4);
        Form1.Controls.Add(motorASettings);
        Form1.Controls.Add(motorBSettings);
        Form1.Controls.Add(button2);
        Form1.Controls.Add(button3);
        Form1.Controls.Add(button4);
        Form1.Controls.Add(ABox);
        Form1.Controls.Add(BBox);
        Form1.Controls.Add(APWMBox);
        Form1.Controls.Add(BPWMBox);
        Form1.ShowDialog();
        serialPort1.DtrEnable = true;
        serialPort1.BaudRate = 500000;
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            serialPort1.PortName = portList.Text;

            if (serialPort1 != null)
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    serialPort1.Open();
                    serialPort1.ReadTimeout = 16;
                    MessageBox.Show("Closing port, because it was already open!", "Port already open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    serialPort1.Open();
                    serialPort1.ReadTimeout = 16;
                    MessageBox.Show("Port Opened", "Port Opened", MessageBoxButtons.OK);
                }
            }
            else
            {
                if (serialPort1.IsOpen)
                {
                    MessageBox.Show("Port already open", "Port already open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("No port Selected", "No port Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }
        
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        button1.Enabled = false;
        button4.Enabled = true;
        button2.Enabled = true;
        button3.Enabled = true;
        motorASettings.Enabled = true;
        motorBSettings.Enabled = true;
    }

    private void motorASettings_SelectedIndexChanged(object sender, EventArgs e)
    {
        MA = motorASettings.Text;
        ABox.Visible = true;
        button2.Visible = true;
        if (MA == "PWM")
        {
            APWMBox.Visible = true;
        }
    }

    private void motorBSettings_SelectedIndexChanged(object sender, EventArgs e)
    {
        MB = motorBSettings.Text;
        BBox.Visible = true;
        button3.Visible = true;
        if (MB == "PWM")
        {
            BPWMBox.Visible = true;
        }
    }

    private void Button2_Click(object sender, EventArgs e)
    {
        if (ABox.Text == "")
        {
            MessageBox.Show("You did not enter a value", "No Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            chosenvalueAI1 = Int32.Parse(ABox.Text);
            if (APWMBox.Visible == true)
            {
                chosenvalueAI2 = Int32.Parse(APWMBox.Text);
            }
            MotorASetting();
        }
    }

    private void Button3_Click(object sender, EventArgs e)
    {
        if (BBox.Text == "")
        {
            MessageBox.Show("You did not enter a value", "No Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            chosenvalueBI1 = Int32.Parse(BBox.Text);
            if (BPWMBox.Visible == true)
            {
                chosenvalueAI2 = Int32.Parse(BPWMBox.Text);
            }
            MotorBSetting();
        }   
    }

    private void Button4_Click(object sender, EventArgs e)
    {
        if (serialPort1.IsOpen)
        {
            button1.Enabled = true;
            button4.Enabled = false;
            button4.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            motorASettings.Enabled = false;
            motorBSettings.Enabled = false;
            serialPort1.Close();
        }
        else
        {

        }
    }

    private void MotorBSetting()
    {
        if (MB == "Potential Term")
        {
            PTSettingsB();
        }
        if (MB == "Integral Term")
        {
            ITSettingsB();
        }
        if (MB == "Derivative Term")
        {
            DTSettingsB();
        }
        if (MB == "Smoothing Term")
        {
            STSettingsB();
        }
        if (MA == "PWM")
        {
            PWMB();
        }

    }

    private void MotorASetting()
    {
        if (MA == "Potential Term")
        {
            PTSettingsA();
        }
        if (MA == "Integral Term")
        {
            ITSettingsA();
        }
        if (MA == "Derivative Term")
        {
            DTSettingsA();
        }
        if (MA == "Smoothing Term")
        {
            STSettingsA();
        }
        if (MA == "PWM")
        {
            PWMA();
        }
    }

    private void PTSettingsA()
    {

        try
        {
            string byte3 = chosenvalueAI1.ToString("X4").Substring(0, 2);
            string byte4 = chosenvalueAI1.ToString("X4").Substring(2, 2);

            PotentialMotorA[2] = Convert.ToByte(byte3, 16);
            PotentialMotorA[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(PotentialMotorA, 0, PotentialMotorA.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void ITSettingsA()
    {
        try
        {

            string byte3 = chosenvalueAI1.ToString("X4").Substring(0, 2);
            string byte4 = chosenvalueAI1.ToString("X4").Substring(2, 2);

            IntegralMotorA[2] = Convert.ToByte(byte3, 16);
            IntegralMotorA[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(IntegralMotorA, 0, IntegralMotorA.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void DTSettingsA()
    {
        try
        {

            string byte3 = chosenvalueAI1.ToString("X4").Substring(0, 2);
            string byte4 = chosenvalueAI1.ToString("X4").Substring(2, 2);

            DerivativeMotorA[2] = Convert.ToByte(byte3, 16);
            DerivativeMotorA[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(DerivativeMotorA, 0, DerivativeMotorA.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void STSettingsA()
    {
        try
        {

            string byte3 = chosenvalueAI1.ToString("X4").Substring(0, 2);
            string byte4 = chosenvalueAI1.ToString("X4").Substring(2, 2);

            SmoothingMotorA[2] = Convert.ToByte(byte3, 16);
            SmoothingMotorA[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(SmoothingMotorA, 0, SmoothingMotorA.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void PWMA()
    {
        try
        {
            string byte3 = chosenvalueAI1.ToString("X2");
            string byte4 = chosenvalueAI2.ToString("X2");

            PWMMotorA[2] = Convert.ToByte(byte3, 16);
            PWMMotorA[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(PWMMotorA, 0, PWMMotorA.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void PTSettingsB()
    {
        try
        {

            string byte3 = chosenvalueBI1.ToString("X4").Substring(0, 2);
            string byte4 = chosenvalueBI1.ToString("X4").Substring(2, 2);

            PotentialMotorB[2] = Convert.ToByte(byte3, 16);
            PotentialMotorB[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(PotentialMotorB, 0, PotentialMotorB.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void ITSettingsB()
    {
        try
        {
            string byte3 = chosenvalueBI1.ToString("X4").Substring(0, 2);
            string byte4 = chosenvalueBI1.ToString("X4").Substring(2, 2);

            IntegralMotorB[2] = Convert.ToByte(byte3, 16);
            IntegralMotorB[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(IntegralMotorB, 0, IntegralMotorB.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void DTSettingsB()
    {
        try
        {
            string byte3 = chosenvalueBI1.ToString("X4").Substring(0, 2);
            string byte4 = chosenvalueBI1.ToString("X4").Substring(2, 2);

            DerivativeMotorB[2] = Convert.ToByte(byte3, 16);
            DerivativeMotorB[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(DerivativeMotorB, 0, DerivativeMotorB.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void STSettingsB()
    {
        try
        {
            string byte3 = chosenvalueBI1.ToString("X4").Substring(0, 2);
            string byte4 = chosenvalueBI1.ToString("X4").Substring(2, 2);

            SmoothingMotorB[2] = Convert.ToByte(byte3, 16);
            SmoothingMotorB[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(SmoothingMotorB, 0, SmoothingMotorB.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void PWMB()
    {
        try
        {
            string byte3 = chosenvalueBI1.ToString("X2");
            string byte4 = chosenvalueBI2.ToString("X2");

            PWMMotorB[2] = Convert.ToByte(byte3, 16);
            PWMMotorB[3] = Convert.ToByte(byte4, 16);

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(PWMMotorB, 0, PWMMotorB.Length);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }



    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenFormWindow();
        }
	}

    void OnApplicationQuit()
    {
        serialPort1.Close();
    }
}

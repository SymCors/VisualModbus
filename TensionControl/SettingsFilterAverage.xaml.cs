﻿using EasyModbus;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TensionControl
{
    /// <summary>
    /// Interaction logic for SettingsFilterAverage.xaml
    /// </summary>
    public partial class SettingsFilterAverage : Window
    {
        SnackbarMessageQueue myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
        ModbusClient modbusClient = Vars.client;

        public SettingsFilterAverage(int sent)
        {
            InitializeComponent();
            Snackbar.MessageQueue = myMessageQueue;
            kp.Text = sent.ToString();
        }

        private void HandleOKMethod()
        {

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            writeSingleData();
        }

        private void kp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                writeSingleData();
        }

        private void writeSingleData()
        {
            try
            {
                int value = Convert.ToInt32(kp.Text.ToString());
                if (value > 40 || value < 1)
                {
                    myMessageQueue.Enqueue("1 - 40 arası bir değer giriniz", "OK", () => HandleOKMethod());
                    return;
                }
                modbusClient.WriteSingleRegister(22, value);
                MessageBox.Show("Yazıldı");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

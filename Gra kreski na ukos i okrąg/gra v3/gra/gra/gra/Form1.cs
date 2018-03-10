using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//biblioteki dodatkowe.:
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace gra
{
    public partial class Form1 : Form
    {
        public char[] pole = new char[10]; // polo [0] odpowiada za to, kogo kolor, a pola 1-9 oznaczają numerki na planszy jak w telefonie
        byte[] odebrane = new byte[10]; //tablica na odbieranie informacji za pomocą serwera (BUFOR)
        bool turn = true; // jeżeli prawda to kolej X'a
        //int turn_count = 0;
        //---------------------------------------pobieranie adresu IP z pola ----------------
        public string getIP()
        {
            return IP.Text;
        }
        //---------------------------------------pobieranie portu ---------------------------
        public int getPort()
        {
            int pom;
            pom = Convert.ToInt32(Port.Text);
            return pom;
        }
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //---------------------------------------funkcja łączenia z serwerem: ---------------
        public void polacz(string IP, int Port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            try
            {
                sock.Connect(endPoint);
            }
            catch(System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Ooops nie można się połączyć z serwerem sprawdź poprawność wpisanych danych", "Connection Error!1");
            }
        }
        void czyszczenieTablicy()
        {
            pole[0] = 'O';
            for(int i=1; i<10; i++)
            {
                pole[i] = 'p';
            }
        }
        public Form1()//------------------------------------------------MAJN
        {
            InitializeComponent();
            przesylanie.Visible = false;
            czyszczenieTablicy();
            turn = true; //zerowanie kogo ruch <--w tym wypadku O
            //turn_count = 0; //zerowanie ilości wykonanych ruchów
            /* try //odblokowanie wszystkich guzików
            {
              foreach (Control c in Controls) //petla dla wszystkich przycisków
                 {
                        Button b = (Button)c;
                        b.Enabled = true;
                        b.Text = "";
                 }
            }
            catch { };
         */
            A1.Enabled = true;
            A2.Enabled = true;
            A3.Enabled = true;

            B1.Enabled = true;
            B2.Enabled = true;
            B3.Enabled = true;

            C1.Enabled = true;
            C2.Enabled = true;
            C3.Enabled = true;

            if (sock.Connected)
            {
                wyslij();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gracz z portem 555 rozpoczyna jako kółko, natomiast gracz z portem 666 jest X'em!", "About"); //po przecinku tytuł
        }

        //----------------------------------------------------------------------------- QUIT -----------------------
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            przesylanie.Visible = true;
            wyslij();
            sock.Close();
            Application.Exit();
        }

        /*private void button_click(object sender, EventArgs e) //klikanie na polach i reakcja #ogólna główna funkcja gry
        {
            Button b = (Button)sender;
            if(pole[0]==1)
            {
                b.Text = "X";
            }
            else
            {
                b.Text = "O";
            }
            turn = !turn;
            b.Enabled = false;
            turn_count++;
            checkForWinner();
            wypelnij();
            uzupelnij();
            wyslij();
        }*/
        public void wypelnij()//-----------------------------------------------------Wypelnij pola danymi z gry-------------------
        {
            if (turn == true)
            {
                pole[0] = 'O';
            }
            else
            {
                pole[0] = 'X';
            }
            if (A1.Text == "")
            {
                pole[1] = 'p';
                //A1.Enabled = true;
            }
            if (A1.Text == "X")
            {
                pole[1] = 'x';
                //A1.Enabled = false;
            }
            if (A1.Text == "O")
            {
                pole[1] = 'o';
                //A1.Enabled = false;
            }
            //--------------------------------------2
            if (A2.Text == "")
            {
                pole[2] = 'p';
                //A2.Enabled = true;
            }
            if (A2.Text == "X")
            {
                pole[2] = 'x';
                //A2.Enabled = false;
            }
            if (A2.Text == "O")
            {
                pole[2] = 'o';
                //A2.Enabled = false;
            }
            //--------------------------------------3
            if (A3.Text == "")
            {
                pole[3] = 'p';
                //A3.Enabled = true;
            }
            if (A3.Text == "X")
            {
                pole[3] = 'x';
                //A3.Enabled = false;
            }
            if (A3.Text == "O")
            {
                pole[3] = 'o';
                //A3.Enabled = false;
            }
            //--------------------------------------4
            if (B1.Text == "")
            {
                pole[4] = 'p';
                //B1.Enabled = true;
            }
            if (B1.Text == "X")
            {
                pole[4] = 'x';
                //B1.Enabled = false;
            }
            if (B1.Text == "O")
            {
                pole[4] = 'o';
                //B1.Enabled = false;
            }
            //-------------------------------------5
            if (B2.Text == "")
            {
                pole[5] = 'p';
                //B2.Enabled = true;
            }
            if (B2.Text == "X")
            {
                pole[5] = 'x';
                //B2.Enabled = false;
            }
            if (B2.Text == "O")////////////////////////////////////////////błąd! naprawiony
            {
                pole[5] = 'o';
                //B2.Enabled = false;
            }
            //------------------------------------6
            if (B3.Text == "")
            {
                pole[6] = 'p';
                //B3.Enabled = true;
            }
            if (B3.Text == "X")
            {
                pole[6] = 'x';
                //B3.Enabled = false;
            }
            if (B3.Text == "O")
            {
                pole[6] = 'o';
                //B3.Enabled = false;
            }
            //-----------------------------------7
            if (C1.Text == "")
            {
                pole[7] = 'p';
                //C1.Enabled = true;
            }
            if (C1.Text == "X")
            {
                pole[7] = 'x';
                //C1.Enabled = false;
            }
            if (C1.Text == "O")
            {
                pole[7] = 'o';
                //C1.Enabled = false;
            }
            //-----------------------------------8
            if (C2.Text == "")
            {
                pole[8] = 'p';
                //C2.Enabled = true;
            }
            if (C2.Text == "X")
            {
                pole[8] = 'x';
                //C2.Enabled = false;
            }
            if (C2.Text == "O")
            {
                pole[8] = 'o';
                //C2.Enabled = false;
            }
            //---------------------------------9
            if (C3.Text == "")
            {
                pole[9] = 'p';
                //C3.Enabled = true;
            }
            if (C3.Text == "X")
            {
                pole[9] = 'x';
                //C3.Enabled = false;
            }
            if (C3.Text == "O")
            {
                pole[9] = 'o';
                //C3.Enabled = false;
            }
        }
        //---------------------------------------------------------------------------Znajdz zwyciezce -----------------------------
        public void checkForWinner()
        {
            bool there_is_a_winner = false;
            
            //sprawdzenie horyzontalnie
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled)) there_is_a_winner = true;
            if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled)) there_is_a_winner = true;
            if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled)) there_is_a_winner = true;
            //sprawdzenie wertykalne
            if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled)) there_is_a_winner = true;
            if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled)) there_is_a_winner = true;
            if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled)) there_is_a_winner = true;
            //sprawdzenie po X'ie ^^
            if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled)) there_is_a_winner = true;
            if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!A3.Enabled)) there_is_a_winner = true;

            

            if (there_is_a_winner)
            {
                disableButtons();
                String Winner = "";
                if(pole[0]=='O')
                {
                    Winner = "O";
                }
                else
                {
                    Winner = "X";
                }
                MessageBox.Show("The winner is " + Winner, "Who Wins");
            }
            else
            {
                bool check_buttons = false;
                if (A1.Enabled == false && A2.Enabled == false && A3.Enabled==false &&
                    B1.Enabled==false && B2.Enabled==false && B3.Enabled==false &&
                    C1.Enabled==false && C2.Enabled==false && C3.Enabled==false)
                {
                    if (!there_is_a_winner)
                    {
                        check_buttons = true;
                    }
                }
                if(check_buttons)
                {
                    MessageBox.Show("There is no Winner HERE!", "Ooops!");
                }
            }
            
            void disableButtons()//dezaktywacja przycisków po wygranej
            {
                /*
                try
                {
                    foreach (Control c in Controls) //petla dla wszystkich przycisków
                    {
                        Button b = (Button)c;
                        b.Enabled = false;
                    }
                }
                catch { };*/
                A1.Enabled = false;
                A2.Enabled = false;
                A3.Enabled = false;
                B1.Enabled = false;
                B2.Enabled = false;
                B3.Enabled = false;
                C1.Enabled = false;
                C2.Enabled = false;
                C3.Enabled = false;
            }
            wyslij();
        }
        public void czyszczenie_gry()
        {
            A1.Text = "";
            A2.Text = "";
            A3.Text = "";

            B1.Text = "";
            B2.Text = "";
            B3.Text = "";

            C1.Text = "";
            C2.Text = "";
            C3.Text = "";
        }
        public void odblokowanie()
        {
            A1.Enabled = true;
            A2.Enabled = true;
            A3.Enabled = true;

            B1.Enabled = true;
            B2.Enabled = true;
            B3.Enabled = true;

            C1.Enabled = true;
            C2.Enabled = true;
            C3.Enabled = true;
        }
        //--------------------------------------------------------------------------- NEW GAME ------------------------------------
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            //wylaczenie odbierania
            przesylanie.Visible = true;
            //czyszczenie tablicy
            czyszczenieTablicy();
            //odblokowanie
            odblokowanie();
            //czyszczenie gry
            czyszczenie_gry();
            przesylanie.Visible = false;
            if (sock.Connected)
            {
                wyslij();
            }
            
        }
        //----------------------------------------------------------------------------Wyślij --------------------------------------
        public void wyslij()
        {
            wypelnij();
            byte[] buffer = new byte[10];
            for (int i = 0; i < 10; i++)
            {
                buffer[i] = Convert.ToByte(pole[i]);
            }
            try
            {
                sock.Send(buffer, 0, buffer.Length, 0);
            }
            catch(System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Ooops nie można się połączyć z serwerem sprawdź poprawność wpisanych danych", "Connection Error!2");
                Application.ExitThread();
            }
            if(przesylanie.Visible==false)
            {
                odbierz();
            }
        }
        //----------------------------------------------------------------------------Odbierz --------------------------------------
        public void odbierz()   
        {
            if (sock.Connected)
            {
                do
                {
                    try
                    {
                        sock.Receive(odebrane, 0, odebrane.Length, 0);
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        MessageBox.Show("Ooops nie można się połączyć z serwerem sprawdź poprawność wpisanych danych", "Connection Error!3");
                        Application.ExitThread();
                    }
                } while (odebrane[0] == 0);
                for (int i = 0; i < 10; i++)
                {
                    pole[i] = Convert.ToChar(odebrane[i]);
                }
            }
            uzupelnij();//pola odebranymi danymi
            sprawdz(); // sprawdz czy można coś wpisać, czy jest jakiś zwyciezca jak jest, to blokuj guziki! SZYBKO!
            label4.Visible = true;
            void sprawdz()
            {
                bool jest = false;
                //sprawdzenie horyzontalnie
                if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled)) jest = true;
                if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled)) jest = true;
                if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled)) jest = true;
                //sprawdzenie wertykalne
                if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled)) jest = true;
                if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled)) jest = true;
                if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled)) jest = true;
                //sprawdzenie po X'ie ^^
                if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled)) jest = true;
                if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!A3.Enabled)) jest = true;
                if (jest)
                {
                    A1.Enabled = false;
                    A2.Enabled = false;
                    A3.Enabled = false;
                    B1.Enabled = false;
                    B2.Enabled = false;
                    B3.Enabled = false;
                    C1.Enabled = false;
                    C2.Enabled = false;
                    C3.Enabled = false;
                }
            }
        }
        //----------------------------------------------------------------------------Uzupełnij pola odebranymi danymi--------------
        public void uzupelnij()//pola odebranymi danymi
        {
            if (kolej.Text == "X")
            {
                pole[0] = 'X';
            }
            else
            {
                pole[0] = 'O';
            }
            if (A1.Enabled)
            {
                if (pole[1] == 'p')
                {
                    A1.Text = "";
                    A1.Enabled = true;
                }
                if (pole[1] == 'x')
                {
                    A1.Text = "X";
                    A1.Enabled = false;
                }
                if (pole[1] == 'o')
                {
                    A1.Text = "O";
                    A1.Enabled = false;
                }
            }
            //-------------------------------------
            if (A2.Enabled)
            {
                if (pole[2] == 'p')
                {
                    A2.Text = "";
                    A2.Enabled = true;
                }
                if (pole[2] == 'x')
                {
                    A2.Text = "X";
                    A2.Enabled = false;
                }
                if (pole[2] == 'o')
                {
                    A2.Text = "O";
                    A2.Enabled = false;
                }
            }
            //--------------------------------------3------
            if (A3.Enabled)
            {
                if (pole[3] == 'p')
                {
                    A3.Text = "";
                    A3.Enabled = true;
                }
                if (pole[3] == 'x')
                {
                    A3.Text = "X";
                    A3.Enabled = false;
                }
                if (pole[3] == 'o')
                {
                    A3.Text = "O";
                    A3.Enabled = false;
                }
            }
            //-------------------------------------4-------
            if (B1.Enabled)
            {
                if (pole[4] == 'p')
                {
                    B1.Text = "";
                    B1.Enabled = true;
                }
                if (pole[4] == 'x')
                {
                    B1.Text = "X";
                    B1.Enabled = false;
                }
                if (pole[4] == 'o')
                {
                    B1.Text = "O";
                    B1.Enabled = false;
                }
            }
            //-------------------------------------5--------
            if (B2.Enabled)
            {
                if (pole[5] == 'p')
                {
                    B2.Text = "";
                    B2.Enabled = true;
                }
                if (pole[5] == 'x')
                {
                    B2.Text = "X";
                    B2.Enabled = false;
                }
                if (pole[5] == 'o')
                {
                    B2.Text = "O";
                    B2.Enabled = false;
                }
            }
            //------------------------------------6----------
            if (B3.Enabled)
            {
                if (pole[6] == 'p')
                {
                    B3.Text = "";
                    B3.Enabled = true;
                }
                if (pole[6] == 'x')
                {
                    B3.Text = "X";
                    B3.Enabled = false;
                }
                if (pole[6] == 'o')
                {
                    B3.Text = "O";
                    B3.Enabled = false;
                }
            }
            //-----------------------------------7----------
            if (C1.Enabled)
            {
                if (pole[7] == 'p')
                {
                    C1.Text = "";
                    C1.Enabled = true;
                }
                if (pole[7] == 'x')
                {
                    C1.Text = "X";
                    C1.Enabled = false;
                }
                if (pole[7] == 'o')
                {
                    C1.Text = "O";
                    C1.Enabled = false;
                }
            }
            //------------------------------------8----------
            if (C2.Enabled)
            {
                if (pole[8] == 'p')
                {
                    C2.Text = "";
                    C2.Enabled = true;
                }
                if (pole[8] == 'x')
                {
                    C2.Text = "X";
                    C2.Enabled = false;
                }
                if (pole[8] == 'o')
                {
                    C2.Text = "O";
                    C2.Enabled = false;
                }
            }
            //------------------------------------9-----------
            if (C3.Enabled)
            {
                if (pole[9] == 'p')
                {
                    C3.Text = "";
                    C3.Enabled = true;
                }
                if (pole[9] == 'x')
                {
                    C3.Text = "X";
                    C3.Enabled = false;
                }
                if (pole[9] == 'o')
                {
                    C3.Text = "O";
                    C3.Enabled = false;
                }
            }
        }
        //----------------------------------------------------------------------------ŁĄCZENIE!-------------------------------------
        private void laczenie_Click(object sender, EventArgs e)
        {
            polacz(getIP(), getPort());
            turn = true; //zerowanie kogo ruch <--w tym wypadku X
            //turn_count = 0; //zerowanie ilości wykonanych ruchów
            //czyszczenieTablicy();
            przesylanie.Visible = false;
            uzupelnij();
            laczenie.Enabled = false;
            if(getPort()==555 && sock.Connected)
            {
                MessageBox.Show("połączono!, ---> TWOJ RUCH! <---", "hurra!");
                kolej.Text = "O";
                label4.Visible = true;
            }
            if (getPort()==666 && sock.Connected)
            {
                kolej.Text = "X";
                label4.Visible = false;
                MessageBox.Show("połączono!, czekasz na ---> TWOJ RUCH! <---", "hurra!");
                odbierz();
            }
        }
   //---------------------------------------------------------------------------------KLIKACZE -----------------------------
        private void A1_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                A1.Text = "O";
            }
            else
            {
                A1.Text = "X";
            }
            A1.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }

        private void A2_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                A2.Text = "O";
            }
            else
            {
                A2.Text = "X";
            }
            A2.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }

        private void A3_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                A3.Text = "O";
            }
            else
            {
                A3.Text = "X";
            }
            A3.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }

        private void B1_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                B1.Text = "O";
            }
            else
            {
                B1.Text = "X";
            }
            B1.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }

        private void B2_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                B2.Text = "O";
            }
            else
            {
                B2.Text = "X";
            }
            B2.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }

        private void B3_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                B3.Text = "O";
            }
            else
            {
                B3.Text = "X";
            }
            B3.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }

        private void C1_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                C1.Text = "O";
            }
            else
            {
                C1.Text = "X";
            }
            C1.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }

        private void C2_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                C2.Text = "O";
            }
            else
            {
                C2.Text = "X";
            }
            C2.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }

        private void C3_Click(object sender, EventArgs e)
        {
            if (pole[0] == 'O')
            {
                C3.Text = "O";
            }
            else
            {
                C3.Text = "X";
            }
            C3.Enabled = false;
            label4.Visible = false;
            checkForWinner();
            turn = !turn;
            wyslij();
        }
    }
}

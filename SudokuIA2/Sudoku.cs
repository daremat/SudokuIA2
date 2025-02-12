﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuIA2
{
    class Sudoku
    {

        private readonly int[][] initialSudoku;  //Sudoku original et vide (ne peut etre modifié)
        private int[][] wokrkingSudoku;  //Sudoku sur lequel vous allez travailler 


        /*--------------------Constructeur--------------------*/
        public Sudoku()  //Constructeur
        {
            initialSudoku = new int[9][];
            wokrkingSudoku = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                initialSudoku[i] = new int[9];
                wokrkingSudoku[i] = new int[9];
            }
            //---------------------------A COMPLETER---------------------------------------------------------------------------------------------------------A COMPLETER
            String init = "003020600900305001001806400008102900700000008006708200002609500800203009005010300";

            initialSudoku = stringToSudoku(init);
            wokrkingSudoku = stringToSudoku(init);
        }

        /*--------------------Getter & Setter--------------------*/

        public int[][] getInitialSudoku(int[][] sudoku)  //Recupére le sudoku initiale
        {
            sudoku = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                sudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i][j] = initialSudoku[i][j];
                }
            }
            return sudoku;
        }

        public int getCaseInitialSudoku(int line, int column)  //Recupére une case du sudoku initiale
        {
            return initialSudoku[line][column];
        }

        public int[][] getSudoku(int[][] sudoku)  //Recupére le sudoku de "travail"
        {
            sudoku = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                sudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i][j] = wokrkingSudoku[i][j];
                }
            }
            return sudoku;
        }
        public int getCaseSudoku(int line, int column)  //Recupére une case du sudoku de "travail"
        {
            return wokrkingSudoku[line][column];
        }

        public bool setSudoku(int[][] sudoku)  //Attribue un nouveau sudoku de "travail" 
        {
            if (!checkSudoku(sudoku, "setSudoku"))  //Renvoie false si ce n'est pas autorisé
                return false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    wokrkingSudoku[i][j] = sudoku[i][j];
                }
            }
            return true;
        }

        public bool setCaseSudoku(int line, int column, int value)  //Attribue une nouvelle case au sudoku de "travail"
        {
            if (!checkCase(line, column, value, "setCaseSudoku"))  //Renvoie false si ce n'est pas autorisé
                return false;

            wokrkingSudoku[line][column] = value;

            return true;
        }

        /*--------------------Affichage--------------------*/
        public void showInitialSudoku()  //Affiche le du sudoku initiale
        {
            show(initialSudoku);
        }

        public bool showSudoku()  //Affiche le sudoku de "travail"
        {
            if (!show(wokrkingSudoku))
                return false;
            return true;
        }


        public bool show(int[][] sudoku)  //Affiche un sudoku
        {
            if (!checkSudoku(sudoku, "show"))  //Renvoie false si il y a un probleme 
                return false;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n");
            for (int i = 0; i < 9; i++)
            {
                if (i == 3 || i == 6)
                    Console.WriteLine("        ---+---+---");
                Console.Write("        ");
                for (int j = 0; j < 9; j++)
                {
                    if (j == 3 || j == 6)
                        Console.Write("|");
                    if (sudoku[i][j] == 0)
                        Console.Write(".");
                    else
                    {
                        if (sudoku[i][j] != 0 && sudoku[i][j] == initialSudoku[i][j])
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(sudoku[i][j]);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                            Console.Write(sudoku[i][j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
            return true;
        }

        /*--------------------Validation du Sudoku--------------------*/

        public bool validationSudoku()  //Valide le sudoku de "travail"
        {
            if (!validation(wokrkingSudoku))  //Renvoie false si il y a un probleme 
                return false;
            return true;
        }
        public bool validation(int[][] sudoku)  //Valide un sudoku  /*--------------------A Optimiser--------------------*/
        {
            if (!checkSudoku(sudoku, "validation"))  //Renvoie false si il y a un probleme 
                return false;

            bool error = false;

            for (int i = 0; i < 9; i++)  //Validation des lignes
            {
                int[] list9 = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    list9[j] = sudoku[i][j];
                }
                if (!validationList9(list9, ("ligne " + i)))
                    error = true;
            }

            for (int j = 0; j < 9; j++)  //Validation des colonnes
            {
                int[] list9 = new int[9];
                for (int i = 0; i < 9; i++)
                {
                    list9[i] = sudoku[i][j];
                }
                if (!validationList9(list9, ("colonne " + j)))
                    error = true;
            }

            for (int ii = 0; ii < 3; ii++)  //Validation des blocs
            {
                for (int jj = 0; jj < 3; jj++)
                {
                    int[] list9 = new int[9];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            list9[i * 3 + j] = sudoku[ii * 3 + i][jj * 3 + j];
                        }
                    }
                    if (!validationList9(list9, ("bloc [" + ii + "][" + jj + "]")))
                        error = true;
                }
            }

            if (error)
            {
                Console.WriteLine("        !!! ECHEC !!! : Ce sudoku n'est pas validé");
                return false;
            }
            else
                Console.WriteLine("        !!! FELICITATION !!! : Ce sudoku est validé");
            return true;
        }

        /*--------------------Outils--------------------*/

        public int[][] stringToSudoku(String stringSudoku)  //Transforme un String en sudoku (tableau de int[9][9])
        {//---------------------------A COMPLETER---------------------------------------------------------------------------------------------------------A COMPLETER  (taille du string, prendre en compte les points et tirets)
            int[][] sudoku;
            sudoku = new int[9][];

            for (int i = 0; i < 9; i++)
            {
                sudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i][j] = stringSudoku[i * 9 + j] - 48; // - 48 pour la conversion de la table ascii
                }
            }
            return sudoku;
        }

        public bool checkSudoku(int[][] sudoku, String log)  //Verifie la validité d'un sudoku (taille 9x9) puis chaque case
        {
            if (sudoku.Length != 9)
            {
                Console.WriteLine("        !!! WARNING !!! : Nombre de lignes incorrect (" + sudoku.Length + ", au lieu de 9) lors de la commande " + log);
                return false;
            }
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i].Length != 9)
                {
                    Console.WriteLine("        !!! WARNING !!! : Nombre de colonnes incorrect a la ligne " + i + " (" + sudoku.Length + ", au lieu de 9) lors de la commande " + log);
                    return false;
                }
                for (int j = 0; j < 9; j++)
                {
                    if (!checkCase(i, j, sudoku[i][j], log))
                        return false;
                }
            }
            return true;
        }

        public bool checkCase(int line, int column, int value, String log)  //Verifie la validité d'une case de sudoku (valeur compris entre 0 et 9 et conforme au sudoku initiale)
        {
            if (initialSudoku[line][column] != 0 && value != initialSudoku[line][column])
            {
                Console.WriteLine("        !!! WARNING !!! : Cette case ne peut etre modifié, c'est une case fixé par le sudoku. (case [" + line + "][" + column + "]) lors de la commande " + log);
                return false;
            }

            if (value < 0 || value > 9)
            {
                Console.WriteLine("        !!! WARNING !!! : Valeur non valable à la case [" + line + "][" + column + "] (" + value + ", au lieu de [0,1,2,3,4,5,6,7,8,9]) lors de la commande " + log);
                return false;
            }

            return true;
        }

        public bool validationList9(int[] list9, String log)  //Valide qu'une list contient bien les 9 chiffres attendu
        {
            if (list9.Length != 9)
            {
                Console.WriteLine("        !!! WARNING !!! : Nombre d'éléments incorect (" + log + ")");
                return false;
            }

            bool flag;  //Tri de la liste
            do
            {
                flag = false;
                for (int k = 0; k < 8; k++)
                {
                    if (list9[k] > list9[k + 1])
                    {
                        int buffer = list9[k];
                        list9[k] = list9[k + 1];
                        list9[k + 1] = buffer;
                        flag = true;
                    }
                }
            } while (flag);

            for (int k = 0; k < 9; k++)
            {
                if (list9[k] == 0)  //Pas de 0 dans le sudoku
                {
                    Console.WriteLine("        !!! ERROR !!! : Solution non valide : il y a encore un 0 (" + log + ")");
                    return false;
                }
                if (k != 8)
                    if (list9[k] == list9[k + 1])  //Pas de doublons dans le sudoku
                    {
                        Console.WriteLine("        !!! ERROR !!! : Solution non valide : il y a un doublons (" + log + ")");
                        return false;
                    }
            }
            return true;
        }
    }
}



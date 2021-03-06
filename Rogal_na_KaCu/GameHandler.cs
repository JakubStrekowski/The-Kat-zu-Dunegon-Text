﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Rogal_na_KaCu
{
   public class GameHandler
    {
        public int enemiesKilled;
        private int gold;
        public int floorNumber;
        Map currentMap;
        DisplayConsole display;
        Input input;
        public Hero hero;
        int whatInControl = 0; //0-hero, 1-game menu, 2-death menu, 3-start menu, 4 ending

        public GameHandler(DisplayConsole display)
        {
            this.display = display;
            input = new Input();
            floorNumber = 1;
        }

        public void SetGold(int value)
        {
            gold = value;
            display.SetStatUI(6, gold.ToString());
        }
        public void AddGold(int value)
        {
            gold += value;
            display.SetStatUI(6, gold.ToString());
        }

        public void CreateHero(string name)
        {
            hero = new Hero(2, 0, 0, null);
            enemiesKilled = 0;
            gold = 0;
            hero.SetName(name);
        }


        void ResolveTurn()
        {
            foreach (Enemy enemy in currentMap.enemies)
            {
                if (hero.isAlife && !enemy.AlreadyMoved)
                {
                    enemy.MovementBehaviour();
                }
            }
            foreach (Enemy enemy in currentMap.enemies)
            {
                enemy.ReenableMove();
            }
        }

        public void NextLevel()
        {
            floorNumber++;
        }

        public Map GenerateRandom(int floorNumber)
        {
            Random rnd = new Random();
            if (floorNumber == 5)
            {
                Map newMap1 = LoadMap("2.txt");
                return newMap1;
            }
            int[][] dungeon=new int[100][];
            dungeon=SetDungeonSize(floorNumber);
            Map newMap = new Map(dungeon, display, this,dungeon.Length,dungeon[0].Length);
            display.DrawFrame();
            currentMap = newMap;
            hero.SetCurrentMap(currentMap);
            ChangeFloorNumber(floorNumber);
            display.SetStatUI(1, hero.name);
            display.SetStatUI(2, hero.hp.ToString());
            display.SetStatUI(6, gold.ToString());
            display.SetStatUI(7, enemiesKilled.ToString());
            bool displayed = false;
            for (int i = 0; i < 6; i++)
                if (hero.equipment[i] != null)
                {
                    display.RefreshItem(i, hero.equipment[i].name);
                    displayed = true;
                    break;
                }
            if (!displayed)
            {
                display.RefreshItem(-1, "Whatever");
            }
            whatInControl = 0;
            currentMap.SetFocus();
            return newMap;
        }

    private int[][] SetDungeonSize(int floorNumber)
        {
            DungeonGenerator mapGenerator = new DungeonGenerator(100, 50);
            int rowAmmount = 0;
            int columnAmmount = 0;
            switch (floorNumber)
            {
                case 1:
                    rowAmmount = 25;
                    columnAmmount = 50;
                    return mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 8);
                case 2:
                    rowAmmount = 35;
                    columnAmmount = 70;
                    return mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 13);
                case 3:
                    rowAmmount = 45;
                    columnAmmount = 85;
                    return  mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 16);
                case 4:
                    rowAmmount = 50;
                    columnAmmount = 100;
                    return mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 20);
            }
            return mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 20); ;
        }

    public Map LoadMap(string name="1.txt")
        {
            display.DrawFrame();
            int mapRowLimit=50;
            int mapColumnLimit = 100;
            int[][] intMap = new int[mapRowLimit][];
            intMap = CreateIntMap(name, mapRowLimit, mapColumnLimit);
            Map newMap = new Map(intMap, display, this, mapRowLimit, mapColumnLimit);
            display.DrawFrame();
            currentMap = newMap;
            hero.SetCurrentMap(currentMap);
            ChangeFloorNumber(floorNumber);
            display.SetStatUI(1, hero.name);
            display.SetStatUI(2, hero.hp.ToString());
            display.SetStatUI(6, gold.ToString());
            display.SetStatUI(7, enemiesKilled.ToString());
            bool displayed = false;
            for (int i = 0; i < 6; i++)
                if (hero.equipment[i] != null)
                {
                    display.RefreshItem(i, hero.equipment[i].name);
                    displayed = true;
                    break;
                }
            if (!displayed)
            {
                display.RefreshItem(-1, "Whatever");
            }
            whatInControl = 0;
            currentMap.SetFocus();
            return newMap;
        }

        private int[][] CreateIntMap(string name, int mapRowLimit, int mapColumnLimit)
        {
            int[][] intMap = new int[mapRowLimit][];
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("maps/" + name);
            int rowCounter = 0;
            while (rowCounter != mapRowLimit)
                if ((line = file.ReadLine()) != null)
                {
                    string[] strRow = line.Split(' ');
                    int[] intRow = new int[mapColumnLimit];
                    int counter = 0;
                    foreach (string st in strRow)
                    {
                        intRow[counter] = int.Parse(st);
                        counter++;
                    }
                    while (counter < mapColumnLimit)
                    {
                        intRow[counter] = 0;
                        counter++;
                    }
                    intMap[rowCounter] = intRow;
                    rowCounter++;
                }
                else
                {
                    while (rowCounter < mapRowLimit)
                    {
                        intMap[rowCounter] = new int[mapColumnLimit];
                        int counter = 0;
                        while (counter < mapColumnLimit)
                        {
                            intMap[rowCounter][counter] = 0;
                            counter++;
                        }
                        rowCounter++;
                    }
                }
            file.Close();
            return intMap;
        }

        public void PlayInMap()
        {
            whatInControl = 0;
            bool heroAlife = true;
            while (heroAlife)
            {
                if (ResolveInput(input.TakeInput()))
                {
                    ResolveTurn();
                }
            }
        }

        public bool ResolveInput(String inputCommand)
        {
            if (whatInControl == 0)
                switch (inputCommand) {
                
                case "ArrowUp":
                    {
                        hero.Move(0);
                    }
                    return true;
                case "ArrowDown":
                    {
                        hero.Move(1);
                    }
                    return true;
                case "ArrowRight":
                    {
                        hero.Move(2);
                    }
                    return true;
                case "ArrowLeft":
                    {
                        hero.Move(3);
                    }
                    return true;
                    case "1":
                        {
                            hero.UseItem(1);
                            return true;
                        }
                    case "2":
                        {
                            hero.UseItem(2);
                            return true;
                        }
                    case "3":
                        {
                            hero.UseItem(3);
                            return true;
                        }
                    case "4":
                        {
                            hero.UseItem(4);
                            return true;
                        }
                    case "5":
                        {
                            hero.UseItem(5);
                            return true;
                        }
                    case "6":
                        {
                            hero.UseItem(6);
                            return true;
                        }
                    case "Escape":
                case "Q":
                    {
                        whatInControl = 1;
                            display.DisplayMenu();
                    }
                    return false;
                default: return false;
            }
            if (whatInControl == 1)
            {
                switch (inputCommand)
                {
                    case "E":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    case "Escape":
                    case "C":
                        {
                            hero.currentMap.MoveFocus(hero);
                            whatInControl = 0;
                            break;
                        }
                    default:
                        break;
                }
                return false;
            }
            if (whatInControl == 2)
            {
                switch (inputCommand)
                {
                    case "S":
                        CreateHero(hero.name);
                        floorNumber = 1;
                        GenerateRandom(floorNumber);
                        return false;
                    case "E":
                        Environment.Exit(0);
                        return false;
                    default:
                        return false;
                }
            }
            if(whatInControl == 4)
            {
                switch (inputCommand)
                {
                    case "Enter":
                        Environment.Exit(0);
                        return false;
                    default:
                        return false;
                }
            }
            else return false;
        }

        private void ChangeFloorNumber(int value)
        {
            display.SetStatUI(0, floorNumber.ToString());
        }

        public void SetWhatInControl(int value)
        {
            whatInControl = value;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Rogal_na_KaCu
{
    class DungeonGenerator
    {
        private int[][] dungeon;
        private List<Room> rooms;
        private List<Room> connectedRooms;
        private int mapSizeX;
        private int mapSizeY;
        Random rnd;
        public DungeonGenerator(int sizeX,int sizeY)
        {
            mapSizeX = sizeX;
            mapSizeY = sizeY;
            connectedRooms = new List<Room>();
            rooms = new List<Room>();
            rnd = new Random();
            dungeon = new int[sizeY][];
            for(int i = 0; i < sizeY; i++)
            {
                dungeon[i] = new int[sizeX];
                for(int j = 0; j < sizeX; j++)
                {
                    dungeon[i][j] = 1;
                }
            }
        }

        public int[][] CreateDungeon(int sizeX, int sizeY, int roomsAmnt)
        {
            mapSizeX = sizeX;
            mapSizeY = sizeY;
            rooms = new List<Room>();
            rnd = new Random();
            dungeon = new int[sizeY][];
            for (int i = 0; i < sizeY; i++)
            {
                dungeon[i] = new int[sizeX];
                for (int j = 0; j < sizeX; j++)
                {
                    dungeon[i][j] = 1;
                }
            }
            CreateIfPossible(5, 5, 0, 30, 30, false);
            rooms[0].isConnected = true;
            connectedRooms.Add(rooms[0]);
            int selectedRoom;
            while (rooms.Count != roomsAmnt)
            {
                
                selectedRoom = rnd.Next(rooms.Count);
                /*int corridorDir = rnd.Next(4);
                Corridor cor = CreateCorridor(rnd.Next(3, 8), corridorDir, rooms[selectedRoom]);
                */
                int dir = rnd.Next(4);
                /*
                if(corridorDir==0)
                    while(dir==1) dir = rnd.Next(4);
                if (corridorDir == 1)
                    while (dir == 0) dir = rnd.Next(4);
                if (corridorDir == 2)
                    while (dir == 3) dir = rnd.Next(4);
                if (corridorDir == 3)
                    while (dir == 2) dir = rnd.Next(4);
                if (cor == null)
                {
                    continue;
                }
                Point nextPlace = cor.GiveEndPoint(dir);*/
                Point nextPlace;
                nextPlace.X = rnd.Next(0, mapSizeX);
                nextPlace.Y = rnd.Next(0, mapSizeY);
                CreateIfPossible(rnd.Next(6, 12), rnd.Next(6, 12), dir, nextPlace.X, nextPlace.Y, false);
                
            }
            int id = 0;
            foreach (Room room in rooms)
            {
                if (!room.isConnected)
                {
                    int  rand= rnd.Next(connectedRooms.Count);
                    Room rndroom = rooms[rand];
                    while (rndroom == room)
                    {
                        rand = rnd.Next(rooms.Count);
                        rndroom = rooms[rand];
                    }
                    connectedRooms.Add(room);
                    ConnectTwoRooms(room, rndroom);
                    room.AddConnection(rand);
                    rndroom.AddConnection(id);
                    room.isConnected = true;
                    rndroom.isConnected = true;
                }
                id++;
            }

            /*
            CheckIntegrity(0, 0);
            int id1 = 0;
            foreach(Room room in rooms)
            {
                if (!room.visited)
                {
                    int rndID = rnd.Next(connectedRooms.Count);
                    ConnectTwoRooms(room, connectedRooms[rndID]);
                    Console.WriteLine("Rooom " + room.beginX + " " + room.beginY + " wasnt visited, connecting to " + connectedRooms[rndID].beginX+" " + connectedRooms[rndID].beginY);
                }
                id1++;
            }
            */
            int randomConnections = rnd.Next(2, 8);
            for(int i = 0; i < randomConnections; i++)
            {
                int room1 = rnd.Next(connectedRooms.Count);
                int room2 = rnd.Next(connectedRooms.Count);
                if (room1 == room2)
                {
                    room2 = rnd.Next(connectedRooms.Count);
                }
                ConnectTwoRooms(connectedRooms[room1], connectedRooms[room2]);
            }

            for(int i = 1; i < rooms.Count; i++)
            {
                int enemyAmnt = rnd.Next(0, 5);
                for(int j = 0; j < enemyAmnt; j++)
                {
                    Point pnt = rooms[i].RandomPointFromRoom();
                    if (rnd.Next(2) == 1)
                        dungeon[pnt.Y][pnt.X] = 3;
                    else dungeon[pnt.Y][pnt.X] = 6;

                }
            }

            StreamWriter sw = new StreamWriter("test.txt");
            PutHero();
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    sw.Write(dungeon[i][j] + " ");
                }
                sw.Write(Environment.NewLine);
            }
            sw.Close();
            
            return dungeon;

        }

        public class Room
        {
            public int beginX;
            public int sizeX;
            public int beginY;
            public int sizeY;
            public bool isConnected;
            public bool visited;
            private Random rnd;
            public List<int> connectedRooms;

            public Room(int bx, int sx, int by, int sy)
            {
                visited = false;
                connectedRooms = new List<int>();
                rnd = new Random();
                beginX = bx;
                sizeX = sx;
                beginY = by;
                sizeY = sy;
                isConnected = false;
            }

            public Point PointFromWall(int direction)
            {
                Point point;
                point.X = 0;
                point.Y = 0;
                switch (direction)
                {
                    case 0: point.X = rnd.Next(beginX+2, beginX + sizeX-2);
                        point.Y = beginY;
                        return point;
                    case 1: point.X= rnd.Next(beginX+2, beginX + sizeX-2);
                        point.Y = beginY + sizeY;
                        return point;
                    case 2: point.X = beginX + sizeX;
                        point.Y = rnd.Next(beginY + 2, beginY + sizeY - 2);
                        return point;
                    case 3:
                        point.X = beginX;
                        point.Y = rnd.Next(beginY + 2, beginY + sizeY - 2);
                        return point;
                }
                return point;
            }

            public void AddConnection(int roomID)
            {
                connectedRooms.Add(roomID);
            }

            public Point RandomPointFromRoom()
            {
                Point pnt;
                pnt.X = rnd.Next(beginX + 1, beginX + sizeX - 1);
                pnt.Y = rnd.Next(beginY + 1, beginY + sizeY - 1);
                return pnt;
            }
        }

        public void CheckIntegrity(int roomID, int distanceFromSpawn)
        {
            if(!rooms[roomID].visited){
                int checkedRoom = roomID;
                rooms[roomID].visited = true;
                connectedRooms.Add(rooms[roomID]);
                distanceFromSpawn++;
                foreach (int connectedRoom in rooms[checkedRoom].connectedRooms)
                {
                    CheckIntegrity(connectedRoom,distanceFromSpawn);
                }
            }
            
        }

        /*
        public class Corridor
        {
            public Point begin;
            public Point end;
            public int direction;
            public Corridor(Point begin, Point end, int dir)
            {
                this.begin = begin;
                this.end = end;
                direction = dir;
            }

            public Point GiveEndPoint(int direction)
            {
                Point point;
                point = end;
                switch (direction)
                {
                    case 0:
                        point.X = end.X;
                        point.Y = end.Y-1;
                        break;
                    case 1:
                        point.X = end.X;
                        point.Y = end.Y+1;
                        break;
                    case 2:
                        point.X = end.X+1;
                        point.Y = end.Y;
                        break;
                    case 3:
                        point.X = end.X-1;
                        point.Y = end.Y;
                        break;
                }
                return point;
            }

            
        }*/

        public struct Point
        {
            public int X;
            public int Y;
        }

        
        /*
        public void CleanCorridor(Corridor cor)
        {
            Point currentPoint = cor.end;
            int dir = cor.direction;
            while(isCorridor(currentPoint.X,currentPoint.Y))
            {
                switch (dir)
                {
                    case 0:
                        dungeon[currentPoint.Y][currentPoint.X] = 1;
                        currentPoint.Y += 1;
                        break;
                    case 1:
                        dungeon[currentPoint.Y][currentPoint.X] = 1;
                        currentPoint.Y -= 1;
                        break;
                    case 2:
                        dungeon[currentPoint.Y][currentPoint.X] = 1;
                        currentPoint.X -= 1;
                        break;
                    case 3:
                        dungeon[currentPoint.Y][currentPoint.X] = 1;
                        currentPoint.Y += 1;
                        break;
                }
            }
        }*/
        /*
        private bool isCorridor(int x, int y)
        {
            int neighborCounter = 4;
            if (dungeon[y + 1][x] != 1) neighborCounter--;
            if (dungeon[y - 1][x] != 1) neighborCounter--;
            if (dungeon[y][x-1] != 1) neighborCounter--;
            if (dungeon[y][x+1] != 1) neighborCounter--;
            if (neighborCounter > 1) return false;
            return true;
        }
        */
        private bool CreateIfPossible(int sizeX, int sizeY, int direction, int beginX, int beginY,bool corridor)
        {
            int sourceX = beginX;
            int sourceY = beginY;
            bool possible = true;
            switch (direction)
            {
                case 0:
                    beginY -= (sizeY-1);
                    beginX =beginX+ rnd.Next(-(sizeX - 2) / 2, (sizeX - 2) / 2);
                    break;
                case 1:
                    beginY -= 1;
                    beginX = beginX + rnd.Next(-((sizeX - 2) / 2), ((sizeX - 2) / 2));
                    break;
                case 2:
                    beginX += 1;
                    beginY =beginY+ rnd.Next(-(sizeY - 2) / 2, (sizeY - 2) / 2);
                    break;
                case 3:
                    beginX -= (sizeX-1);
                    beginY = beginY + rnd.Next(-((sizeX - 2) / 2), ((sizeX - 2) / 2));
                    break;
                default:
                    break;
            }
            if (!(beginX >= 0) || !(beginY >= 0)||sizeX+beginX>mapSizeX||sizeY+beginY>mapSizeY)
            {
                return false;
            }
            for (int i = beginY; i < sizeY + beginY; i++)
            {
                for (int j = beginX; j < sizeX + beginX; j++)
                {
                    if (dungeon[i][j] != 1)
                    {
                        return false;
                    }
                }
            }
            if (!possible) return false;
            for (int i = beginY; i < sizeY + beginY; i++)
            {
                for (int j = beginX; j < sizeX + beginX; j++)
                {
                    if (!corridor&&(i == 0 || j == 0 || i == beginY + sizeY - 1 || j == beginX + sizeX - 1))
                    {
                        dungeon[i][j] = 1;
                    }
                    else
                    {
                        dungeon[i][j] = 0;
                    }
                }
            }
            if(!corridor)rooms.Add(new Room(beginX, sizeX, beginY, sizeY));
            return true;
        }
        /*
        public Corridor CreateCorridor(int length, int direction, Room sourceRoom)
        {
            Point possibleBegin = sourceRoom.PointFromWall(direction);
            switch (direction)
            {
                case 0:
                    dungeon[possibleBegin.Y + 1][possibleBegin.X] = 0;
                    break;
                case 1:
                    dungeon[possibleBegin.Y -1][possibleBegin.X] = 0;
                    break;
                case 2:
                    dungeon[possibleBegin.Y][possibleBegin.X-1] = 0;
                    break;
                case 3:
                    dungeon[possibleBegin.Y][possibleBegin.X + 1] = 0;
                    break;
            }
            if (direction == 0 || direction == 1)
            {
                 if(CreateIfPossible(1, length, direction, possibleBegin.X, possibleBegin.Y,true)){
                    Point end;
                    end.X = possibleBegin.X;
                    end.Y = possibleBegin.Y + length;
                    Corridor cor = new Corridor(possibleBegin,end,direction);
                    sourceRoom.AddToCorridorList(cor);
                    return cor;
                }
                 else
                    if(direction==0) Console.WriteLine("nie moge narysowac korytarza w gore");
                    else Console.WriteLine("nie moge narysowac korytarza dol");
            }
            else
            {
                if (CreateIfPossible(length, 1, direction, possibleBegin.X, possibleBegin.Y,true))
                {
                    Point end;
                    end.X = possibleBegin.X+length;
                    end.Y = possibleBegin.Y;
                    Corridor cor = new Corridor(possibleBegin, end, direction);
                    sourceRoom.AddToCorridorList(cor);
                    return cor;
                }
                else
                    if(direction==2) Console.WriteLine("nie moge narysowac korytarza w prawo");
                else Console.WriteLine("nie moge narysowac korytarza w lewo");
            }
            Console.WriteLine("Cos pojszlo ni tak.");
            return null;
            
            
        }*/

        
        
        private void ConnectTwoRooms(Room room1, Room room2)
        {
            Point currentPoint;
            Point pointRoom1;
            Point pointRoom2;
            pointRoom1.X = rnd.Next(room1.beginX+1, room1.beginX + room1.sizeX-1);
            pointRoom1.Y = rnd.Next(room1.beginY+1, room1.beginY + room1.sizeY-1);
            pointRoom2.X = rnd.Next(room2.beginX+1, room2.beginX + room2.sizeX-1);
            pointRoom2.Y = rnd.Next(room2.beginY+1, room2.beginY + room2.sizeY-1);
            currentPoint.X = pointRoom2.X;
            currentPoint.Y = pointRoom2.Y;
            int distanceX = MeasureDistance(currentPoint, pointRoom1).X;
            int distanceY = MeasureDistance(currentPoint, pointRoom1).Y;
            bool upFirst = (rnd.Next(2) == 1) ? true : false;

            Point possibleRoute;
            bool madeStep = false;
            while(!(distanceX==0&&distanceY==0))
            {
                madeStep = false;
                possibleRoute.X = currentPoint.X;
                possibleRoute.Y = currentPoint.Y;
                if (upFirst)
                {
                    possibleRoute.Y -= 1;
                    if (MeasureDistance(pointRoom1, possibleRoute).Y < distanceY && !madeStep)
                    {
                        distanceY = MeasureDistance(pointRoom1, possibleRoute).Y;
                        dungeon[possibleRoute.Y][possibleRoute.X] = 0;
                        currentPoint = possibleRoute;
                        madeStep = true;
                    }
                    else
                    {
                        possibleRoute.Y += 2;
                        if (MeasureDistance(pointRoom1, possibleRoute).Y < distanceY&&!madeStep)
                        {
                            distanceY = MeasureDistance(pointRoom1, possibleRoute).Y;
                            dungeon[possibleRoute.Y][possibleRoute.X] = 0;
                            currentPoint = possibleRoute;
                            madeStep = true;
                        }
                    }
                    possibleRoute.Y -= 1;
                    possibleRoute.X -= 1;
                    if(MeasureDistance(pointRoom1, possibleRoute).X < distanceX && !madeStep)
                    {
                        distanceX = MeasureDistance(pointRoom1, possibleRoute).X;
                        dungeon[possibleRoute.Y][possibleRoute.X] = 0;
                        currentPoint = possibleRoute;
                        madeStep = true;
                    }
                    else
                    {
                        possibleRoute.X += 2;
                        if (MeasureDistance(pointRoom1, possibleRoute).X < distanceX && !madeStep)
                        {
                            distanceX = MeasureDistance(pointRoom1, possibleRoute).X;
                            dungeon[possibleRoute.Y][possibleRoute.X] = 0;
                            currentPoint = possibleRoute;
                            madeStep = true;
                        }
                    }
                    possibleRoute.X -= 1;
                }
                else
                {
                    possibleRoute.X -= 1;
                    if (MeasureDistance(pointRoom1, possibleRoute).X < distanceX && !madeStep)
                    {
                        distanceX = MeasureDistance(pointRoom1, possibleRoute).X;
                        dungeon[possibleRoute.Y][possibleRoute.X] = 0;
                        currentPoint = possibleRoute;
                        madeStep = true;
                    }
                    else
                    {
                        possibleRoute.X += 2;
                        if (MeasureDistance(pointRoom1, possibleRoute).X < distanceX && !madeStep)
                        {
                            distanceX = MeasureDistance(pointRoom1, possibleRoute).X;
                            dungeon[possibleRoute.Y][possibleRoute.X] = 0;
                            currentPoint = possibleRoute;
                            madeStep = true;
                        }
                    }
                    possibleRoute.X -= 1;
                    possibleRoute.Y -= 1;
                    if (MeasureDistance(pointRoom1, possibleRoute).Y < distanceY && !madeStep)
                    {
                        distanceY = MeasureDistance(pointRoom1, possibleRoute).Y;
                        dungeon[possibleRoute.Y][possibleRoute.X] = 0;
                        currentPoint = possibleRoute;
                        madeStep = true;
                    }
                    else
                    {
                        possibleRoute.Y += 2;
                        if (MeasureDistance(pointRoom1, possibleRoute).Y < distanceY && !madeStep)
                        {
                            distanceY = MeasureDistance(pointRoom1, possibleRoute).Y;
                            dungeon[possibleRoute.Y][possibleRoute.X] = 0;
                            currentPoint = possibleRoute;
                            madeStep = true;
                        }
                    }
                    possibleRoute.Y -= 1;
                }

                if (rnd.Next(1, 101) > 90)
                {
                    upFirst = !upFirst;
                }
            }
        }
        

        private Point MeasureDistance(Point a, Point b)
        {
            Point distance;
            if( a.X < b.X){
                distance.X = b.X - a.X;
            }
            else
            {
                distance.X = a.X - b.X;
            }
            if (a.Y < b.Y)
            {
                distance.Y = b.Y - a.Y;
            }
            else
            {
                distance.Y = a.Y - b.Y;
            }
            return distance;
        }

        private void PutHero()
        {
            int posX = rnd.Next(rooms[0].beginX + 1, rooms[0].beginX + rooms[0].sizeX-1);
            int posY = rnd.Next(rooms[0].beginY + 1, rooms[0].beginY + rooms[0].sizeY-1);
            dungeon[posY][posX] = 2;
        }
        
    }
}

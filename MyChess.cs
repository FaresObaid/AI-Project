using System;
using System.Collections.Generic;

namespace MyChess
{
    class CPE480
    {
        static int[][] EvaPawn = new int[][]
        {
            new int[] { 0,  0,  0,  0,  0,  0,  0,  0 },
            new int[] { 50, 50, 50, 50, 50, 50, 50, 50 },
            new int[] { 10, 10, 20, 30, 30, 20, 10, 10 },
            new int[] { 5,  5, 10, 25, 25, 10,  5,  5 },
            new int[] { 0,  0,  0, 20, 20,  0,  0,  0 },
            new int[] { 5, -5,-10,  0,  0,-10, -5,  5 },
            new int[] { 5, 10, 10,-20,-20, 10, 10,  5 },
            new int[] { 0,  0,  0,  0,  0,  0,  0,  0}
        };
        static int[][] EvaBishop = new int[][]
        {
            new int[] {-20,-10,-10,-10,-10,-10,-10,-20 },
            new int[] { -10,  0,  0,  0,  0,  0,  0,-10 },
            new int[] { -10,  0,  5, 10, 10,  5,  0,-10 },
            new int[] { -10,  5,  5, 10, 10,  5,  5,-10 },
            new int[] { -10,  0, 10, 10, 10, 10,  0,-10 },
            new int[] { -10, 10, 10, 10, 10, 10, 10,-10 },
            new int[] { -10,  5,  0,  0,  0,  0,  5,-10 },
            new int[] { -20,-10,-10,-10,-10,-10,-10,-20 }
         };
        static int[][] EvaRook = new int[][]
        {
            new int[] {0,  0,  0,  0,  0,  0,  0,  0 },
            new int[] {  5, 10, 10, 10, 10, 10, 10,  5 },
            new int[] { -5,  0,  0,  0,  0,  0,  0, -5 },
            new int[] { -5,  0,  0,  0,  0,  0,  0, -5 },
            new int[] { -5,  0,  0,  0,  0,  0,  0, -5 },
            new int[] { -5,  0,  0,  0,  0,  0,  0, -5 },
            new int[] { -5,  0,  0,  0,  0,  0,  0, -5 },
            new int[] {  0,  0,  0,  5,  5,  0,  0,  0 }
         };

        static int[] dxr = { 1, -1, 0, 0 };
        static int[] dyr = { 0, 0, 1, -1 };
        static int[] dxb = { 1, 1, -1, -1 };
        static int[] dyb = { 1, -1, 1, -1 };
        static bool flag = false, f1 = false, f2 = false;
        static int c;
        static int IntialDepth = 6;
        static int tm = 0;

        public static bool CheckRange(int val, int mn, int mx)
        {
            return (val >= mn && val <= mx);
        }
        public static bool initiator(int[][] Position)
        {
            int[][] p = new int[][]
            {
                new int[] { 6, 0, 0, 0, 0, 5, 0, 0 },
                new int[] { 4, 4, 4, 4, 4, 4, 4, 4 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 2, 0, 0, 0, 0, 3 }
            };

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (Position[i][j] != p[i][j]) return false;
                }
            }

            return true;
        }

        public static bool Attack(int piece, int color)
        {
            if (CheckRange(piece, 1 + (color == 1 ? 1 : 0) * 3, 3 + (color == 1 ? 1 : 0) * 3)) return true;
            return false;
        }
        public static string convert(int x1, int y1, int x2, int y2)
        {

            char[] c = new char[4];
            c[0] = (char)(y1 + 'a');
            c[1] = (char)(x1 + '0');
            c[2] = (char)(y2 + 'a');
            c[3] = (char)(x2 + '0');
            string s = "";
            s = string.Join("", c);

            return s;
        }
        public static List<string> RookMove(int[][] Position, int color, int px, int py)
        {
            List<string> ret = new List<string>();

            for (int i = 0; i < 4; ++i)
            {
                int tx = px, ty = py;
                while (true)
                {
                    tx += dxr[i];
                    ty += dyr[i];
                    if (!(CheckRange(tx, 0, 7) && CheckRange(ty, 0, 7) && (Position[tx][ty] == 0 || Attack(Position[tx][ty], color)))) break;
                    if (Position[tx][ty] == 0) ret.Add(convert(px, py, tx, ty));
                    else
                    {
                        ret.Add(convert(px, py, tx, ty));
                        break;
                    }

                }
            }
            return ret;
        }
        public static List<string> PawnMove(int[][] Position, int color, int px, int py)
        {
            List<string> ret = new List<string>();

            if (CheckRange(px + -1 * color, 0, 7) && Position[px + -1 * color][py] == 0)
                ret.Add(convert(px, py, px + -1 * color, py));
            if (CheckRange(px + -2 * color, 0, 7) && Position[px + -2 * color][py] == 0 && (px == 1 && Position[px][py] == 4 || px == 6 && Position[px][py] == 1))
                ret.Add(convert(px, py, px + -2 * color, py));
            if (CheckRange(px + -1 * color, 0, 7) && py < 7 && Attack(Position[px + -1 * color][py + 1], color))
                ret.Add(convert(px, py, px + -1 * color, py + 1));
            if (CheckRange(px + -1 * color, 0, 7) && py > 0 && Attack(Position[px + -1 * color][py - 1], color))
                ret.Add(convert(px, py, px + -1 * color, py - 1));

            return ret;
        }
        public static List<string> BishopMove(int[][] Position, int color, int px, int py)
        {
            List<string> ret = new List<string>();

            for (int i = 0; i < 4; ++i)
            {
                int tx = px, ty = py;
                while (true)
                {
                    tx += dxb[i];
                    ty += dyb[i];
                    if (!(CheckRange(tx, 0, 7) && CheckRange(ty, 0, 7) && (Position[tx][ty] == 0 || Attack(Position[tx][ty], color)))) break;
                    if (Position[tx][ty] == 0) ret.Add(convert(px, py, tx, ty));
                    else
                    {
                        ret.Add(convert(px, py, tx, ty));
                        break;
                    }

                }
            }
            return ret;
        }

        public static int Evaluation(int[][] Position, int color)
        {
            int Eva = 0;
            int w = 0, b = 0;
            for (int k = 0; k < 8; ++k)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (CheckRange(Position[k][j], 1, 3)) ++w;
                    if (CheckRange(Position[k][j], 4, 6)) ++b;
                    if (Position[k][j] == 1) Eva += (color == 1 ? 1 : -1) * (100 + EvaPawn[k][j]);
                    if (Position[k][j] == 2) Eva += (color == 1 ? 1 : -1) * (300 + EvaBishop[k][j]);
                    if (Position[k][j] == 3) Eva += (color == 1 ? 1 : -1) * (500 + EvaRook[k][j]);
                    if (Position[k][j] == 4) Eva += (color == -1 ? 1 : -1) * (100 + EvaPawn[7 - k][7 - j]);
                    if (Position[k][j] == 5) Eva += (color == -1 ? 1 : -1) * (300 + EvaBishop[7 - k][7 - j]);
                    if (Position[k][j] == 6) Eva += (color == -1 ? 1 : -1) * (500 + EvaRook[7 - k][7 - j]);
                }
            }
            if (w == 0 && color == -1) Eva += 10000;
            if (b == 0 && color == 1) Eva += 10000;
            return Eva;
        }
        public static void NextMove(int[][] Position, int color, List<string> all)
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (CheckRange(Position[i][j], 1, 3) && color == 1)
                    {
                        if (Position[i][j] == 1) all.AddRange(PawnMove(Position, color, i, j));
                        if (Position[i][j] == 3) all.AddRange(RookMove(Position, color, i, j));
                        if (Position[i][j] == 2) all.AddRange(BishopMove(Position, color, i, j));
                    }
                    if (CheckRange(Position[i][j], 4, 6) && color == -1)
                    {
                        if (Position[i][j] == 4) all.AddRange(PawnMove(Position, color, i, j));
                        if (Position[i][j] == 6) all.AddRange(RookMove(Position, color, i, j));
                        if (Position[i][j] == 5) all.AddRange(BishopMove(Position, color, i, j));
                    }
                }
            }
        }
        public static int AlphaBetaMax(int[][] Position, int color, ref string BestMove, int alpha, int beta, int depth)
        {
            if (depth == 0) return Evaluation(Position, color);

            List<string> all = new List<string>();
            NextMove(Position, color, all);
            if (all.Count == 0)
            {
                int score = Evaluation(Position, color);
                if (score >= beta) return beta - 10000;
                if (score > alpha) alpha = score - 10000;
            }


            for (int i = 0; i < all.Count; ++i)
            {
                int y1 = all[i][0] - 'a';
                int x1 = all[i][1] - '0';
                int y2 = all[i][2] - 'a';
                int x2 = all[i][3] - '0';
                int temp = Position[x2][y2], tmp2 = Position[x1][y1];

                Position[x2][y2] = Position[x1][y1]; Position[x1][y1] = 0;
                if (Position[x2][y2] == 4 && x2 == 7) Position[x2][y2] = 6;
                if (Position[x2][y2] == 1 && x2 == 0) Position[x2][y2] = 3;

                int score = AlphaBetaMin(Position, color, ref BestMove, alpha, beta, depth - 1);
                Position[x1][y1] = tmp2; Position[x2][y2] = temp;

                if (score >= beta) return beta;
                if (score > alpha)
                {
                    if (depth == IntialDepth) BestMove = all[i];
                    alpha = score;
                }
            }

            return alpha;
        }
        public static int AlphaBetaMin(int[][] Position, int color, ref string BestMove, int alpha, int beta, int depth)
        {
            if (depth == 0) return Evaluation(Position, color);

            List<string> all = new List<string>();
            NextMove(Position, (color == 1 ? -1 : 1), all);
            if (all.Count == 0)
            {
                int score = Evaluation(Position, color);
                if (score <= alpha) return alpha - 10000;
                if (score < beta) beta = score - 10000;
            }

            for (int i = 0; i < all.Count; ++i)
            {
                int y1 = all[i][0] - 'a';
                int x1 = all[i][1] - '0';
                int y2 = all[i][2] - 'a';
                int x2 = all[i][3] - '0';
                int temp = Position[x2][y2], tmp2 = Position[x1][y1];

                Position[x2][y2] = Position[x1][y1]; Position[x1][y1] = 0;
                if (Position[x2][y2] == 4 && x2 == 7) Position[x2][y2] = 6;
                if (Position[x2][y2] == 1 && x2 == 0) Position[x2][y2] = 3;

                int score = AlphaBetaMax(Position, color, ref BestMove, alpha, beta, depth - 1);
                Position[x1][y1] = tmp2; Position[x2][y2] = temp;

                if (score <= alpha) return alpha;
                if (score < beta) beta = score;
            }

            return beta;
        }
        public static string Project(int[][] Position)
        {
            string BestMove = "";
            int color;

            if (!flag && initiator(Position) == true)
                c = 1;
            else if (!flag) c = -1;
            flag = true;
            color = c;


            /*int cnt1 = 0, cnt2 = 0;
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (Position[i][j] == 0) continue;
                    if (Position[i][j] == 1 || Position[i][j] == 4) ++cnt1;
                    else ++cnt2;
                }
            }

            if (!f1 && cnt1 <= 8) { ++IntialDepth; f1 = true; }
            if (!f2 && cnt2 <= 2) { ++IntialDepth; f2 = true; }*/

            //DateTime start = DateTime.Now;
            int score = AlphaBetaMax(Position, color, ref BestMove, Int32.MinValue + 100000, Int32.MaxValue - 100000, IntialDepth);
            /*TimeSpan timeItTook = DateTime.Now - start;
            tm += (int)timeItTook.TotalSeconds;
            if (tm >= 150 && IntialDepth == 8) IntialDepth = 7;
            else if (tm >= 150 && IntialDepth == 7) IntialDepth = 6;
            if (tm >= 270) IntialDepth = 5;
            else if (tm >= 270 && IntialDepth == 5) IntialDepth = 4;*/
            
            char[] letters = BestMove.ToCharArray();
            letters[1] = (char)(8 - (BestMove[1] - '0') + '0');
            letters[3] = (char)(8 - (BestMove[3] - '0') + '0');
            BestMove = string.Join("", letters);

            return BestMove;
        }
    }
}

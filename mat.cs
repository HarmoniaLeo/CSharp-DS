using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAGL
{
    public class mat
    {
        protected class rowNode
        {
            public int rowId;
            public double value;
            public rowNode nextRow;
        }

        protected class lineNode
        {
            public int lineId;
            public rowNode firstRow;
            public lineNode nextLine;

            public lineNode()
            {
                firstRow = new rowNode();
                firstRow.rowId = -1;
            }
        }

        protected lineNode baseLine;
        protected int rowCount;
        protected int lineCount;

        protected class iterator
        {
            public rowNode row;
            public lineNode line;
            public rowNode prerow;
            public lineNode preline;

            public void nextRow()
            {
                prerow = row;
                row = row.nextRow;
            }

            public void nextLine()
            {
                preline = line;
                line = line.nextLine;
                if (line != null)
                {
                    prerow = line.firstRow;
                    row = line.firstRow.nextRow;
                }
            }

            public void reRow()
            {
                row = line.firstRow.nextRow;
                prerow = line.firstRow;
            }

            public void addLine(int lineId)
            {
                lineNode newLine = new lineNode();
                newLine.lineId = lineId;
                newLine.nextLine = preline.nextLine;
                preline.nextLine = newLine;
                line = newLine;
                prerow = newLine.firstRow;
                row = null;
            }

            public void addRow(int rowId, double value)
            {
                rowNode newRow = new rowNode();
                newRow.rowId = rowId;
                newRow.nextRow = prerow.nextRow;
                prerow.nextRow = newRow;
                newRow.value = value;
                row = newRow;
            }
        }

        protected iterator it;

        public mat(int line,int row)
        {
            baseLine = new lineNode();
            baseLine.lineId = -1;
            it = new iterator();
            it.preline = baseLine;
            it.line = null;
            it.row = null;
            it.prerow = null;
            lineCount = line;
            rowCount = row;
        }

        protected void reLine()
        {
            it.preline = baseLine;
            it.line = baseLine.nextLine;
            if (it.line != null)
            {
                it.prerow = it.line.firstRow;
                it.row = it.line.firstRow.nextRow;
            }
            else
            {
                it.prerow = null;
                it.row = null;
            }
        }

        public void set(int line, int row, double value)
        {
            if (line < 0 || row < 0 ||line>=lineCount||row>=rowCount)
                return;
            while (true)
            {
                if (it.line == null)
                {
                    it.addLine(line);
                    break;
                }
                if (it.line.lineId == line)
                    break;
                if (line<it.line.lineId&&line>it.preline.lineId)
                {
                    it.nextLine();
                    it.addLine(line);
                    break;
                }
                if (line <= it.preline.lineId)
                {
                    reLine();
                    continue;
                }
                it.nextLine();
            }
            while (true)
            {
                if (it.row == null)
                {
                    if (value == 0)
                        break;
                    it.addRow(row, value);
                    break;
                }
                if (it.row.rowId == row)
                {
                    it.row.value = value;
                    if (it.row.value == 0)
                    {
                        it.row = it.row.nextRow;
                        it.prerow.nextRow = it.row;
                        if (it.line.firstRow.nextRow == null)
                        {
                            it.line = it.line.nextLine;
                            it.preline.nextLine = it.line;
                            it.row = it.line.firstRow.nextRow;
                            it.prerow.nextRow = it.line.firstRow;
                        }
                    }
                    break;
                }
                if (row < it.row.rowId && row > it.prerow.rowId)
                {
                    if (value == 0)
                        break;
                    it.addRow(row, value);
                    break;
                }
                if (row <= it.prerow.rowId)
                {
                    it.reRow();
                    continue;
                }
                it.nextRow();
            }
        }

        public double get(int line, int row)
        {
            if (line < 0 || row < 0 || line >= lineCount || row >= rowCount)
                return 0;
            while (true)
            {
                if (it.line == null)
                    return 0;
                if (it.line.lineId == line)
                    break;
                if (line < it.line.lineId && line > it.preline.lineId)
                    return 0;
                if (line <= it.preline.lineId)
                {
                    reLine();
                    continue;
                }
                it.nextLine();
            }
            while (true)
            {
                if (it.row == null)
                    return 0;
                if (it.row.rowId == row)
                    return it.row.value;
                if (row < it.row.rowId && row > it.prerow.rowId)
                    return 0;
                if (row <= it.prerow.rowId)
                {
                    it.reRow();
                    continue;
                }
                it.nextRow();
            }
        }

        public mat(mat tar)
        {
            baseLine = new lineNode();
            baseLine.lineId = -1;
            it = new iterator();
            it.preline = baseLine;
            it.line = null;
            it.row = null;
            it.prerow = null;
            tar.reLine();
            lineCount = tar.lineCount;
            rowCount = tar.rowCount;
            while (tar.it.line!=null)
            {
                while (tar.it.row != null)
                {
                    set(tar.it.line.lineId, tar.it.row.rowId, tar.it.row.value);
                    tar.it.nextRow();
                }
                tar.it.nextLine();
            }
        }

        public mat(int[][] tar)
        {
            baseLine = new lineNode();
            baseLine.lineId = -1;
            it = new iterator();
            it.preline = baseLine;
            it.line = null;
            it.row = null;
            it.prerow = null;
            for (int i = 0; i < tar.Length; i++)
                for (int j = 0; j < tar[i].Length; j++)
                    set(i, j, tar[i][j]);
        }

        protected void add(int line, int row, double value)
        {
            if (line < 0 || row < 0 || line >= lineCount || row >= rowCount)
                return;
            while (true)
            {
                if (line <= it.preline.lineId)
                {
                    reLine();
                    continue;
                }
                if (it.line == null)
                {
                    it.addLine(line);
                    break;
                }
                if (it.line.lineId == line)
                    break;
                if (line < it.row.rowId && line > it.preline.lineId)
                {
                    it.nextLine();
                    it.addLine(line);
                    break;
                }
                it.nextLine();
            }
            while (true)
            {
                if (row <= it.prerow.rowId)
                {
                    it.reRow();
                    continue;
                }
                if (it.row == null)
                {
                    if (value == 0)
                        break;
                    it.addRow(row, value);
                    break;
                }
                if (it.row.rowId == row)
                {
                    it.row.value += value;
                    if (it.row.value == 0)
                    {
                        it.row = it.row.nextRow;
                        it.prerow.nextRow = it.row;
                        if (it.line.firstRow.nextRow == null)
                        {
                            it.line = it.line.nextLine;
                            it.preline.nextLine = it.line;
                            it.row = it.line.firstRow.nextRow;
                            it.prerow.nextRow = it.line.firstRow;
                        }
                    }
                    break;
                }
                if (row < it.row.rowId && row > it.prerow.rowId)
                {
                    if (value == 0)
                        break;
                    it.addRow(row, value);
                    break;
                }
                it.nextRow();
            }
        }

        protected void times(int line, int row, double value)
        {
            if (line < 0 || row < 0 || line >= lineCount || row >= rowCount)
                return;
            while (true)
            {
                if (line <= it.preline.lineId)
                {
                    reLine();
                    continue;
                }
                if (it.line == null)
                    return;
                if (it.line.lineId == line)
                    break;
                if (line < it.row.rowId && line > it.preline.lineId)
                    return;

                it.nextLine();
            }
            while (true)
            {
                if (row <= it.prerow.rowId)
                {
                    it.reRow();
                    continue;
                }
                if (it.row == null)
                    return;
                if (it.row.rowId == row)
                {
                    it.row.value *= value;
                    if (it.row.value == 0)
                    {
                        it.row = it.row.nextRow;
                        it.prerow.nextRow = it.row;
                        if (it.line.firstRow.nextRow == null)
                        {
                            it.line = it.line.nextLine;
                            it.preline.nextLine = it.line;
                            it.row = it.line.firstRow.nextRow;
                            it.prerow.nextRow = it.line.firstRow;
                        }
                    }
                    return;
                }
                if (row < it.row.rowId && row > it.prerow.rowId)
                    return;
                it.nextRow();
            }
        }

        static public mat operator+(mat obj,mat tar)
        {
            if (tar.rowCount != obj.rowCount || tar.lineCount != obj.lineCount)
                return new mat(0, 0);
            mat result=new mat(obj);
            tar.reLine();
            while (tar.it.line != null)
            {
                while (tar.it.row != null)
                {
                    result.add(tar.it.line.lineId, tar.it.row.rowId, tar.it.row.value);
                    tar.it.nextRow();
                }
                tar.it.nextLine();
            } 
            return result;
        }

        static public mat operator -(mat obj, mat tar)
        {
            if (tar.rowCount != obj.rowCount || tar.lineCount != obj.lineCount)
                return new mat(0, 0);
            mat result = new mat(obj);
            tar.reLine();
            while (tar.it.line != null)
            {
                while (tar.it.row != null)
                {
                    result.add(tar.it.line.lineId, tar.it.row.rowId, -tar.it.row.value);
                    tar.it.nextRow();
                }
                tar.it.nextLine();
            }
            return result;
        }

        static public mat operator *(mat obj,double value)
        {
            mat result = new mat(obj);
            result.reLine();
            while (result.it.line!= null)
            {
                while (result.it.row != null)
                {
                    result.times(result.it.line.lineId, result.it.row.rowId, value);
                    result.it.nextRow();
                }
                result.it.nextLine();
            }
            return result;
        }

        static public mat operator *(double value,mat obj)
        {
            mat result = new mat(obj);
            result.reLine();
            while (result.it.line != null)
            {
                while (result.it.row != null)
                {
                    result.times(result.it.line.lineId, result.it.row.rowId, value);
                    result.it.nextRow();
                }
                result.it.nextLine();
            }
            return result;
        }

        static public mat operator *(mat tar,mat obj)
        {
            mat result = new mat(tar.lineCount,obj.rowCount);
            tar.reLine();
            obj.reLine();
            while (tar.it.line!=null)
            {
                for (int i = 0; i < obj.rowCount; i++)
                {
                    double sum = 0;
                    while (tar.it.row != null)
                    {
                        sum += tar.it.row.value * obj.get(tar.it.row.rowId, i);
                        tar.it.nextRow();
                    }
                    result.set(tar.it.line.lineId, i,sum);
                    tar.it.reRow();
                }
                tar.it.nextLine();
            }
            return result;
        }

        public mat turn()
        {
            mat result = new mat(rowCount,lineCount);
            reLine();
            while (it.line!= null)
            {
                while (it.row!= null)
                {
                    result.set(it.row.rowId, it.line.lineId,it.row.value);
                    it.nextRow();
                }
                it.nextLine();
            }
            return result;
        }

        public void print()
        {
            reLine();
            int lineNum = 0;
            while(it.line!=null)
            {
                int rowNum = 0;
                while(it.row!=null)
                {
                    for (; rowNum < it.row.rowId; rowNum++)
                        Console.Write("0\t");
                    rowNum++;
                    Console.Write(it.row.value);
                    Console.Write("\t");
                    it.nextRow();
                }
                for(;rowNum<rowCount;rowNum++)
                    Console.Write("0\t");
                Console.Write("\n");
                lineNum++;
                it.nextLine();
            }
            for(;lineNum<lineCount;lineNum++)
            {
                for (int rowNum=0; rowNum < rowCount; rowNum++)
                    Console.Write("0\t");
                Console.Write("\n");
            }
        }
    }

    public class smat : mat
    {
        public smat(int n):base(n,n){}

        public smat(smat tar) : base(tar){}

        static public smat operator +(smat obj, smat tar)
        {
            if (tar.rowCount != obj.rowCount || tar.lineCount != obj.lineCount)
                return new smat(0);
            smat result = new smat(obj);
            tar.reLine();
            while (tar.it.line != null)
            {
                while (tar.it.row != null)
                {
                    result.add(tar.it.line.lineId, tar.it.row.rowId, tar.it.row.value);
                    tar.it.nextRow();
                }
                tar.it.nextLine();
            }
            return result;
        }

        static public smat operator -(smat obj, smat tar)
        {
            if (tar.rowCount != obj.rowCount || tar.lineCount != obj.lineCount)
                return new smat(0);
            smat result = new smat(obj);
            tar.reLine();
            while (tar.it.line != null)
            {
                while (tar.it.row != null)
                {
                    result.add(tar.it.line.lineId, tar.it.row.rowId, -tar.it.row.value);
                    tar.it.nextRow();
                }
                tar.it.nextLine();
            }
            return result;
        }

        static public smat operator *(smat obj, double value)
        {
            smat result = new smat(obj);
            result.reLine();
            while (result.it.line != null)
            {
                while (result.it.row != null)
                {
                    result.times(result.it.line.lineId, result.it.row.rowId, value);
                    result.it.nextRow();
                }
                result.it.nextLine();
            }
            return result;
        }

        static public smat operator *(double value, smat obj)
        {
            smat result = new smat(obj);
            result.reLine();
            while (result.it.line != null)
            {
                while (result.it.row != null)
                {
                    result.times(result.it.line.lineId, result.it.row.rowId, value);
                    result.it.nextRow();
                }
                result.it.nextLine();
            }
            return result;
        }

        static public smat operator *(smat tar, smat obj)
        {
            smat result = new smat(tar.lineCount);
            tar.reLine();
            obj.reLine();
            while (tar.it.line != null)
            {
                for (int i = 0; i < obj.rowCount; i++)
                {
                    double sum = 0;
                    while (tar.it.row != null)
                    {
                        sum += tar.it.row.value * obj.get(tar.it.row.rowId, i);
                        tar.it.nextRow();
                    }
                    result.set(tar.it.line.lineId, i,sum);
                    tar.it.reRow();
                }
                tar.it.nextLine();
            }
            return result;
        }

        new public smat turn()
        {
            smat result = new smat(rowCount);
            reLine();
            while (it.line != null)
            {
                while (it.row != null)
                {
                    result.set(it.row.rowId, it.line.lineId, it.row.value);
                    it.nextRow();
                }
                it.nextLine();
            }
            return result;
        }

        public smat tri()
        {
            smat tmp = new smat(this);
            smat result = new smat(lineCount);
            lineNode maxline = null;
            lineNode max2line = null;
            lineNode premaxline = null;
            lineNode premax2line = null;
            tmp.reLine();
            int i = 0;
            while (tmp.it.line!= null)
            {
                while (tmp.it.line!= null)
                {
                    if (maxline == null || tmp.it.row.rowId < maxline.firstRow.nextRow.rowId)
                    {
                        max2line = maxline;
                        premax2line = premaxline;
                        maxline = tmp.it.line;
                        premaxline = tmp.it.preline;
                    }
                    else if ((max2line == null || tmp.it.row.rowId < max2line.firstRow.nextRow.rowId) && tmp.it.line.lineId != maxline.lineId)
                    {
                        max2line = tmp.it.line;
                        premax2line = tmp.it.preline;
                    }
                    tmp.it.nextLine();
                }
                if (max2line!=null&&maxline.firstRow.nextRow.rowId == max2line.firstRow.nextRow.rowId)
                {
                    double basis = max2line.firstRow.nextRow.value / maxline.firstRow.nextRow.value;
                    rowNode rowit = maxline.firstRow.nextRow;
                    while (rowit != null)
                    {
                        tmp.add(max2line.lineId, rowit.rowId, -(rowit.value * basis));
                        rowit = rowit.nextRow;
                    }
                }
                else
                {
                    premaxline.nextLine = maxline.nextLine;
                    maxline.nextLine = null;
                    if (premax2line == maxline)
                        premax2line = premaxline;
                    rowNode j = maxline.firstRow.nextRow;
                    while(j!=null)
                    {
                        result.set(i, j.rowId, j.value);
                        j = j.nextRow;
                    }
                    i++;
                    result.it.nextLine();
                    maxline = null;
                    premaxline = null;
                    max2line = null;
                    premax2line = null;
                }
                tmp.reLine();
            }
            return result;
        }

        public double det()
        {
            smat result = tri();
            int i = 0;
            double sum = 1;
            result.reLine();
            while (result.it.line != null)
            {
                if (result.it.row.rowId != i)
                    return 0;
                i++;
                sum *= result.it.row.value;
                result.it.nextLine();
            }
            return sum;
        }

        public smat re()
        {
            smat tmp = new smat(this);
            smat tmp2 = new smat(lineCount);
            smat resultTmp = new smat(lineCount);
            smat result = new smat(lineCount);
            for (int l = 0; l < lineCount; l++)
                resultTmp.set(l, l, 1);
            tmp.reLine();
            resultTmp.reLine();
            lineNode maxline = null;
            lineNode max2line = null;
            lineNode premaxline = null;
            lineNode premax2line = null;
            lineNode resMaxline = null;
            lineNode resMax2line = null;
            lineNode preresMaxline = null;
            lineNode preresMax2line = null;
            int i = 0;
            while (tmp.it.line != null)
            {
                while (tmp.it.line != null)
                {
                    if (maxline == null || tmp.it.row.rowId < maxline.firstRow.nextRow.rowId)
                    {
                        max2line = maxline;
                        premax2line = premaxline;
                        resMax2line = resMaxline;
                        preresMax2line = preresMaxline;
                        maxline = tmp.it.line;
                        premaxline = tmp.it.preline;
                        resMaxline = resultTmp.it.line;
                        preresMaxline = resultTmp.it.preline;
                    }
                    else if ((max2line == null || tmp.it.row.rowId < max2line.firstRow.nextRow.rowId) && tmp.it.line.lineId != maxline.lineId)
                    {
                        max2line = tmp.it.line;
                        premax2line = tmp.it.preline;
                        resMax2line = resultTmp.it.line;
                        preresMax2line = resultTmp.it.preline;
                    }
                    tmp.it.nextLine();
                    resultTmp.it.nextLine();
                }
                if (max2line != null && maxline.firstRow.nextRow.rowId == max2line.firstRow.nextRow.rowId)
                {
                    double basis = max2line.firstRow.nextRow.value / maxline.firstRow.nextRow.value;
                    rowNode rowit = maxline.firstRow.nextRow;
                    rowNode resRowit = resMaxline.firstRow.nextRow;
                    while (rowit != null)
                    {
                        add(max2line.lineId, rowit.rowId, -(rowit.value * basis));
                        add(resMax2line.lineId, resRowit.rowId, -(resRowit.value * basis));
                        rowit = rowit.nextRow;
                        resRowit = resRowit.nextRow;
                    }
                }
                else
                {
                    premaxline.nextLine = maxline.nextLine;
                    preresMaxline.nextLine = resMaxline.nextLine;
                    maxline.nextLine = null;
                    resMaxline.nextLine = null;
                    if (premax2line == maxline)
                    {
                        premax2line = premaxline;
                        preresMax2line = preresMaxline;
                    }
                    rowNode j = maxline.firstRow.nextRow;
                    rowNode k = resMaxline.firstRow.nextRow;
                    while (j != null)
                    {
                        tmp2.set(i, j.rowId, j.value/ maxline.firstRow.nextRow.value);
                        j = j.nextRow;
                    }
                    while (k != null)
                    {
                        result.set(i, k.rowId, k.value/ maxline.firstRow.nextRow.value);
                        k = k.nextRow;
                    }
                    i++;
                    tmp2.it.nextLine();
                    result.it.nextLine();
                    maxline = null;
                    resMaxline = null;
                    premaxline = null;
                    preresMaxline = null;
                    max2line = null;
                    resMax2line = null;
                    premax2line = null;
                    preresMax2line = null;
                }
                tmp.reLine();
                resultTmp.reLine();
            }
            if (i != lineCount)
                return new smat(0);
            tmp2.reLine();
            result.reLine();
            for (int l = lineCount - 1; l >= 0; l--)
                for (int j = lineCount - 1; j > l; j--)
                {
                    for(int k=j;k<lineCount;k++)
                        result.add(l, k, -result.get(j, k) * tmp2.get(l, j));
                    tmp2.it.reRow();
                }
            return result;
        }
    }
}

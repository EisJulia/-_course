namespace Lab1;

public class StrategyCalculator
{
    private int _m = 5;

    private List<double[]> _g = new();
    private double[] _h = new double[101];

    public void Calculate()
    {
        for (var i = 0; i < _m; i++)
        {
            var newG = new double[100];
            newG[99] = 1;
            _g.Add(newG);
        }

        _h[100] = 0;

        for (var t = 98; t >= 0; t--)
        {
            var k = 5;
            _g[k - 1][t] = (t - k + 1) / (double)(t + 1) * _g[k - 1][t + 1];
        }

        for (var k = 4; k >= 1; k--)
        {
            for (var t = 98; t >= 0; t--)
            {
                _g[k - 1][t] = k / (double)(t + 1) * _g[k][t + 1] + (t - k + 1) / (double)(t + 1) * _g[k - 1][t + 1];
            }
        }

        for (var t = 99; t >= 0; t--)
        {
            var sum = 0.0;
            for (var k = 1; k <= _m; k++)
            {
                sum += Math.Max(_h[t + 1], _g[k - 1][t]);
            }

            _h[t] = sum / (t + 1) + (t + 1 - _m) * _h[t + 1] / (t + 1);
        }
    }

    public void WriteResultsInFiles()
    {
        var swG1 = new StreamWriter("D:\\Study\\Lab1\\Lab1\\Lab1\\G1.txt");
        var swG2 = new StreamWriter("D:\\Study\\Lab1\\Lab1\\Lab1\\G2.txt");
        var swG3 = new StreamWriter("D:\\Study\\Lab1\\Lab1\\Lab1\\G3.txt");
        var swG4 = new StreamWriter("D:\\Study\\Lab1\\Lab1\\Lab1\\G4.txt");
        var swG5 = new StreamWriter("D:\\Study\\Lab1\\Lab1\\Lab1\\G5.txt");
        var swH = new StreamWriter("D:\\Study\\Lab1\\Lab1\\Lab1\\H.txt");

        WriteFile(swG1, _g[0]);
        WriteFile(swG2, _g[1]);
        WriteFile(swG3, _g[2]);
        WriteFile(swG4, _g[3]);
        WriteFile(swG5, _g[4]);
        
        WriteFile(swH, _h);
        
        swG1.Close();
        swG2.Close();
        swG3.Close();
        swG4.Close();
        swG5.Close();
        swH.Close();
    }

    private void WriteFile(StreamWriter sw, double[] g)
    {
        foreach (var t in g)
        {
            sw.WriteLine(t);
        }
    }
}
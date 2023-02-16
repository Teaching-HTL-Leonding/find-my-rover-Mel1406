#region variables
int positionEastWest = 0;
int positionNorthSouth = 0;
string input;
string northSouth;
string eastWest;
#endregion

System.Console.WriteLine(EvaluatePosition(GetInput()));

#region methods
string GetInput()
{
    do
    {
        System.Console.WriteLine("Where is the Rover? ");
        input = System.Console.ReadLine()!;
    } while (!CheckIfInputValid(input));
    return input;
}

bool CheckIfInputValid(string input)
{
    foreach (char c in input)
    {
        if (!char.IsAsciiDigit(c) && !(c == 'V' || c == '^' || c == '<' || c == '>'))
        {
            return false;
        }
    }
    return true;
}

string EvaluatePosition(string input)
{
    int toAdd;
    int nextValidTurn;
    for (int i = 0; i < input.Length; i++)
    {
        if (i + 2 <= input.Length)
        {
            nextValidTurn = GetPositionOfNextTurn(input.Substring(i + 1));
        }
        else { nextValidTurn = 0; }
        if (nextValidTurn == 0) { toAdd = 1; }
        else
        {
            if (nextValidTurn == -1) { toAdd = int.Parse(input.Substring(i + 1)); }
            else { toAdd = int.Parse(input.Substring(i + 1, (nextValidTurn + i) - i)); }
        }
        switch (input[i])
        {
            case 'V': positionNorthSouth += toAdd; break;
            case '^': positionNorthSouth -= toAdd; break;
            case '>': positionEastWest += toAdd; break;
            case '<': positionEastWest -= toAdd; break;
            default: break;
        }
    }
    if (positionNorthSouth < 0) { northSouth = $"{-positionNorthSouth}m north"; }
    else if (positionNorthSouth > 0) { northSouth = $"{positionNorthSouth}m south"; }
    else { northSouth = "neither north or south"; }
    if (positionEastWest > 0) { eastWest = $"{positionEastWest}m east"; }
    else if (positionEastWest < 0) { eastWest = $"{-positionEastWest}m west"; }
    else { eastWest = "neither east or west"; }

    double distance = Math.Round(Math.Sqrt(Math.Pow(positionNorthSouth, 2) + Math.Pow(positionEastWest, 2)), 2);
    double manhattanDistance = Math.Abs(positionEastWest) + Math.Abs(positionNorthSouth);

    return $"The rover is {northSouth} and {eastWest}. The distance is {distance} and the manhattan distance is {manhattanDistance}.";
}
int GetPositionOfNextTurn(string input) => input.IndexOfAny(new[] { 'V', '^', '<', '>' });
#endregion
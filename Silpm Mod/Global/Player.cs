public static int timeCheck=0;

public void UpdatePlayer()
	{
	//checking CrazerKilled bool
	timeCheck++;
	if(Main.GetKeyState((int)Microsoft.Xna.Framework.Input.Keys.O) < 0)
		{
		if  ((ModWorld.CrazerKilled) && (timeCheck>100))
			{
			Main.NewText("Crazer was killed!");
			timeCheck=0;
			}
		if ((!ModWorld.CrazerKilled) && (timeCheck>100))
			{
			Main.NewText("Crazer wasn't killed!");
			timeCheck=0;
			}
		}
	}
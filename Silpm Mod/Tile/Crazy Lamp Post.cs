bool LightOn=true;

public void Update(int x, int y)
{
    if (LightOn)
		{
		Lighting.addLight((int)(x), (int)(y), 1.63f, 0.73f, 1.64f);
		} else
		{
		Lighting.GetBlackness((int)(x), (int)(y));
		}
}

public void hitWire(int x, int y)
	{
	if(LightOn==true)
		{
		LightOn=false;
		//Lighting.GetBlackness((int)(x), (int)(y));
		} else
	if(LightOn==false)
		{
		LightOn=true;
		}
	}
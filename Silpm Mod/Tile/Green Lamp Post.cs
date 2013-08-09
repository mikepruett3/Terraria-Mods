bool LightOn=true;

public void Update(int x, int y)
{
    if (LightOn)
		{
		Lighting.addLight((int)(x), (int)(y), 0.27f, 2.13f, 0.00f);
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
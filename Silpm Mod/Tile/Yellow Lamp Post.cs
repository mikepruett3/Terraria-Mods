bool LightOn=true;

public void Update(int x, int y)
{
    if (LightOn)
		{
		Lighting.addLight((int)(x), (int)(y), 2.23f, 2.25f, 0.00f);
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
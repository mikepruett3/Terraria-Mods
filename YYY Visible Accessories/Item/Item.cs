
public void Draw_My_Visible_Accessory(Player P , SpriteBatch SP , string Name , int kind)
{
    Vector2 OffsetOf =new Vector2(P.direction*2,0);
    Texture2D QB =  Main.goreTexture[Config.goreID[Name]];
    Rectangle CompareReference = P.legFrame;
    if(kind == 0) CompareReference = P.headFrame;
    if(kind == 1) CompareReference = P.bodyFrame;
    if(kind == 2) CompareReference = P.legFrame;
    SpriteEffects effects;
    SpriteEffects effects2;
    if (P.gravDir == 1f)
    {
        if (P.direction == 1)
        {
            effects = SpriteEffects.None;
            effects2 = SpriteEffects.None;
        }
        else
        {
            effects = SpriteEffects.FlipHorizontally;
            effects2 = SpriteEffects.FlipHorizontally;
        }
    }
    else
    {
        if (P.direction == 1)
        {
            effects = SpriteEffects.FlipVertically;
            effects2 = SpriteEffects.FlipVertically;
        }
        else
        {
            effects = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
            effects2 = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
        }
    }						
    Vector2 PC = P.position+new Vector2(P.width/2,P.height/2);
    Vector2 ID = new Vector2(QB.Width,QB.Height);
    Color Lighter = P.GetImmuneAlpha2(Lighting.GetColor((int)PC.X / 16, (int)PC.Y / 16, Color.White));
    Vector2 origin = new Vector2((float)CompareReference.Width * 0.5f, (float)CompareReference.Height * 0.5f);

    Vector2 Offset = new Vector2(-2*P.direction,0);
    SP.Draw(
    QB, 
    new Vector2((int)(PC.X - Main.screenPosition.X - CompareReference.Width / 2), 
    (int)(P.position.Y - Main.screenPosition.Y + P.height - CompareReference.Height + 4f))
    + P.bodyPosition +Offset +OffsetOf
    + new Vector2((float)(CompareReference.Width / 2), (float)(CompareReference.Height / 2)),
    new Rectangle?(CompareReference), 
    Lighter, 
    P.bodyRotation, 
    origin, 
    1f, 
    effects, 
    0f);
}

public int Add_Custom_Wings(string TextureName)
{  
    Texture2D TEX=Main.goreTexture[Config.goreID[TextureName]];
    for(int i = 0; i < Main.wingsTexture.Length; i++)
    {
        Texture2D T = Main.wingsTexture[i];
        if(TEX == T)
        {
            return i;
        }
    }

    Texture2D[] wingsTextureNew = new Texture2D[Main.wingsTexture.Length+1];
    for (int i = 0; i < Main.wingsTexture.Length; i++)
    {
        wingsTextureNew[i]=Main.wingsTexture[i];
    }
    wingsTextureNew[Main.wingsTexture.Length] = TEX;
    Main.wingsTexture = wingsTextureNew;
    return Main.wingsTexture.Length-1;
}
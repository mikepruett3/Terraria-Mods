

//Vector2 StartPoint;
//Vector2 EndPoint;


//#region AI


//public void AI()
//{

//    #region Aesthetics
//    if (Main.myPlayer == projectile.owner)
//    {
//        if (Main.player[projectile.owner].channel)
//        {
//            float num119 = 3f;
//            Vector2 vector14 = new Vector2(Main.player[projectile.owner].position.X + (float)Main.player[projectile.owner].width * 0.5f, Main.player[projectile.owner].position.Y + (float)Main.player[projectile.owner].height * 0.5f);
//            float num120 = (float)Main.mouseX + Main.screenPosition.X - vector14.X;
//            float num121 = (float)Main.mouseY + Main.screenPosition.Y - vector14.Y;
//            float num122 = (float)Math.Sqrt((double)(num120 * num120 + num121 * num121));
//            num122 = (float)Math.Sqrt((double)(num120 * num120 + num121 * num121));
//            num122 = num119 / num122;
//            num120 *= num122;
//            num121 *= num122;
//            if (num120 != projectile.velocity.X || num121 != projectile.velocity.Y)
//            {
//                projectile.netUpdate = true;
//            }
//            Projectile P = projectile;
//            Vector2 Target = new Vector2(Main.mouseX + Main.screenPosition.X,Main.mouseY + Main.screenPosition.Y);
//            Vector2 PD = new Vector2(P.width,P.height);
//            P.position=Target-PD/2;
//        }
//        else
//        {
//            projectile.Kill();
//        }
//    }
//}


//#endregion

//#endregion

//#region Initialize


//public void Initialize()
//{
//    Projectile P = projectile;
//    Vector2 PC = P.position+new Vector2(P.width/2,P.height/2);
//    StartPoint = PC;
//}


//#endregion


//#region Kill


//public void Kill()
//{
//    Projectile P = projectile;
//    Vector2 PC = P.position+new Vector2(P.width/2,P.height/2);
//    EndPoint = PC;
//    NeededVel = EndPoint-StartPoint;
//    float Projectile_Speed = 10f;
//    DIST = NeededVel.Length();
//    DIST = Projectile_Speed / DIST;
//    NeededVel*=Dist;
    
//}


//#endregion

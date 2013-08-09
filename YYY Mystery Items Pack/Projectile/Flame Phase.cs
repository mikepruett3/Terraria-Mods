public void AI()
{
if (projectile.timeLeft > 60)
   {
       projectile.timeLeft = 60;
   }
   if (projectile.ai[0] > 7f)
   {
       float scalar = 1f;
       if (projectile.ai[0] == 8f)
       {
           scalar = 0.25f;
       }
       else
       {
           if (projectile.ai[0] == 9f)
           {
               scalar = 0.5f;
           }
           else
           {
               if (projectile.ai[0] == 10f)
               {
                   scalar = 0.75f;
               }
           }
       }
       projectile.ai[0] += 1f;
       int dusttype = 6;
       if (dusttype == 6 || Main.rand.Next(2) == 0)
       {
           for (int looptime = 0; looptime < 3; looptime++)
           {
                if(looptime == 0) dusttype = 6;     //change these dust types to make the flames look nice , clay. -6 = fire
                if(looptime == 1) dusttype = 60;    // -60 = a particle , i think belongs to a colored torch
                if(looptime == 2) dusttype = 63;    // -63 = white colored torch or something like that
               int dustpointer = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dusttype, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
               if (Main.rand.Next(3) != 0)
               {
                   Main.dust[dustpointer].noGravity = true;
                   Main.dust[dustpointer].scale *= 3f;
                   Dust DustRef1 = Main.dust[dustpointer];
                   DustRef1.velocity*=2f;
               }
               Main.dust[dustpointer].scale *= 1.5f;
               Dust DustRef3 = Main.dust[dustpointer];
               DustRef3.velocity*=1.2f;
               Main.dust[dustpointer].scale *= scalar;
           }
       }
   }
   else
   {
       projectile.ai[0] += 1f;
   }
   projectile.rotation += 0.3f * (float)projectile.direction;
}   
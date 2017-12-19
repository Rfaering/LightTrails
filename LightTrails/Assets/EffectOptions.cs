using Assets.Models;
public class EffectOptions
{
    public static Effect[] Options =
    {
          Effect.Create("Plex",Effect.EffectKind.Particle, typeof(PlexMenuItem)),
          Effect.Create("Fireworks",Effect.EffectKind.Particle, typeof(FireworksMenuItem)),
          Effect.Create("Wisps",Effect.EffectKind.Particle, typeof(WispMenuItem)),
          Effect.Create("Rain",Effect.EffectKind.Particle),
          Effect.Create("SmallFire", Effect.EffectKind.Particle, typeof(SmallFireMenuItem)),
          Effect.Create("Trails", Effect.EffectKind.Particle, typeof(TrailsMenuItem)),
          Effect.Create("Fire", Effect.EffectKind.Particle,typeof(FireMenuItem)),
          Effect.Create("LargeFire", Effect.EffectKind.Particle),
          Effect.Create("Focus", Effect.EffectKind.Particle, typeof(FocusMenuItem)),
          Effect.Create("Dream",Effect.EffectKind.Particle),
          Effect.Create("StarDust",Effect.EffectKind.Particle, typeof(StartDustMenuItem)),
          Effect.Create("Smoke", Effect.EffectKind.Particle),
          Effect.Create("Hearts",Effect.EffectKind.Particle, typeof(HeartsMenuItem)),
          /*Effect.Create("Explosions", Effect.EffectKind.Particle,typeof(ExplosionMenuItem)),*/
          Effect.Create("Wind", Effect.EffectKind.Particle, typeof(WindMenuItem)),
          Effect.Create("FadingStars", Effect.EffectKind.Particle),
          Effect.Create("Lightning", Effect.EffectKind.Particle, typeof(LightningMenuItem)),
          Effect.Create("FlameThrower", Effect.EffectKind.Particle),
          Effect.Create("Flare", Effect.EffectKind.Particle),
          Effect.Create("Snow", Effect.EffectKind.Particle, typeof(SnowMenuItem)),
          //Effect.Create("LightTrails", Effect.EffectKind.Particle)


          Effect.Create("Brightness", Effect.EffectKind.Shader, typeof(BrightnessMenuItem)),
          Effect.Create("Bars", Effect.EffectKind.Shader, typeof(BarsMenuItem)),
          Effect.Create("Distortion", Effect.EffectKind.Shader, typeof(DistortionMenuItem)),
          Effect.Create("Water", Effect.EffectKind.Shader, typeof(WaterMenuItem)),
          Effect.Create("Mist", Effect.EffectKind.Shader, typeof(MistMenuItem)),
          Effect.Create("Vignetting", Effect.EffectKind.Shader, typeof(VignettingMenuItem)),
          Effect.Create("Voronoi", Effect.EffectKind.Shader),
          Effect.Create("Wave", Effect.EffectKind.Shader, typeof(WaveMenuItem)),
          Effect.Create("Transition", Effect.EffectKind.Shader, typeof(TransitionMenuItem))
          //Effect.Create("Hay", Effect.EffectKind.Shader),
};
}


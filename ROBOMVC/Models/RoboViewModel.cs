﻿using ROBOMVC.Util;

namespace ROBOMVC.Models
{
    public class RoboViewModel
    {
        public HeadViewModel Head { get; set; }
        public ArmViewModel LeftArm { get; set; }
        public ArmViewModel RightArm { get; set; }
    }

    public class HeadViewModel
    {
        public HeadRotation Rotation { get; set; }
        public HeadTilt Tilt { get; set; }
    }

    public class ArmViewModel
    {
        public Elbow Elbow { get; set; }
        public Wrist Wrist { get; set; }
    }

    public enum Elbow
    {
        [Display("Em repouso")]
        AtRest = 0,

        [Display("Levemente contraído")]
        SlightlyContracted = 1,

        [Display("Contraído")]
        Contracted = 2,

        [Display("Fortemente contraído")]
        StronglyContracted = 3
    }

    public enum Wrist
    {
        [Display("Rotação para menos 90")]
        RotationToMinus90 = 0,

        [Display("Rotação para menos 45")]
        RotationToMinus45 = 1,

        [Display("Em repouso")]
        AtRest = 2,

        [Display("Rotação para 45")]
        RotationTo45 = 3,

        [Display("Rotação para 90")]
        RotationTo90 = 4,

        [Display("Rotação para 135")]
        RotationTo135 = 5,

        [Display("Rotação para 180")]
        RotationTo180 = 6
    }

    public enum HeadRotation
    {
        [Display("Rotação para menos 90")]
        RotationToMinus90 = 0,

        [Display("Rotação para menos 45")]
        RotationToMinus45 = 1,

        [Display("Em repouso")]
        AtRest = 2,

        [Display("Rotação para 45")]
        RotationTo45 = 3,

        [Display("Rotação para 90")]
        RotationTo90 = 4
    }

    public enum HeadTilt
    {
        [Display("Para cima")]
        Upward = 0,

        [Display("Em repouso")]
        AtRest = 1,

        [Display("Para baixo")]
        Downward = 2
    }

}

﻿using Bannerlord.UIExtenderEx;
using HarmonyLib;
using RecruitYourOwnCulture.Behaviors;
using RecruitYourOwnCulture.Model;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;


namespace RecruitYourOwnCulture
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (!(game.GameType is Campaign))
                return;
            ((CampaignGameStarter)gameStarterObject).AddBehavior((CampaignBehaviorBase)new NotableBehavior());
            ((CampaignGameStarter)gameStarterObject).AddBehavior((CampaignBehaviorBase)new GarissonBehavior());
            ((CampaignGameStarter)gameStarterObject).AddModel((GameModel)new VolunteerModel());
            ((CampaignGameStarter)gameStarterObject).AddModel((GameModel)new PrisonerRecruitmentCalculationModel());
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            new Harmony("com.dasneo.recruit_own_culture").PatchAll();
            UIExtender uiExtender = new UIExtender("RecruitYourOwnCulture");
            uiExtender.Register(typeof(SubModule).Assembly);
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("Recruit Your Own Culture Loaded", Color.FromUint(4282569842U)));
        }
    }
}

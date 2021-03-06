﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBA.Modules.HealthPlanSurveyService.Models;

namespace UBA.Modules.HealthPlanSurveyService.Services
{
    public class HPSDataController
    {
        #region Syntax

        //CRUD operation + TableName;

        #endregion

        #region SQL Strings
        string SurveyListSql = @"SELECT top 25 r.ResponseId, '' as 'Broker', r.OrganizationName, r.City, s.StateCode, 
                        r.PersonCompletingSurvey, r.PersonCompletingSurvey_Title, r.PersonCompletingSurvey_Email, 
                        r.PersonCompletingSurvey_Phone, r.PersonCompletingSurvey_PhoneExt, 
                        st.[Description] as 'ResponseStatus', r.CompletedBy, r.CreatedOn, r.UpdatedOn, r.CompletedOn  
                        FROM SurveyResponse_General r 
                        INNER JOIN US_State s on r.US_StateId = s.US_StateId 
                        INNER JOIN SurveyResponseStatusType st on r.SurveyResponseStatusTypeId = st.SurveyResponseStatusTypeId 
                        ORDER BY OrganizationName";
        string SurveySummaryModelSql = @"SELECT r.ResponseId, '' as 'Broker', r.OrganizationName, r.City, s.StateCode, 
                        r.PersonCompletingSurvey, r.PersonCompletingSurvey_Title, r.PersonCompletingSurvey_Email, 
                        r.PersonCompletingSurvey_Phone, r.PersonCompletingSurvey_PhoneExt, 
                        st.[Description] as 'ResponseStatus', r.CompletedBy, r.CreatedOn, r.UpdatedOn, r.CompletedOn  
                        FROM SurveyResponse_General r 
                        INNER JOIN US_State s on r.US_StateId = s.US_StateId 
                        INNER JOIN SurveyResponseStatusType st on r.SurveyResponseStatusTypeId = st.SurveyResponseStatusTypeId 
                        WHERE r.ResponseId = @0";
        string SurveyResponseItemSql = @"SELECT [ResponseId]
                            ,[SurveyId]
                            ,[WebID]
                            ,[MemberFirmId]
                            ,[ClientId]
                            ,[NotParticipatingReasonTypeId]
                            ,[SurveyResponseStatusTypeId]
                            ,[CreatedOn]
                            ,[CreatedBy]
                            ,[UpdatedOn]
                            ,[UpdatedBy]
                            ,[CompletedBy]
                            ,[CompletedOn]
                            ,[OrganizationName]
                            ,[PersonCompletingSurvey]
                            ,[PersonCompletingSurvey_Title]
                            ,[PersonCompletingSurvey_Email]
                            ,[PersonCompletingSurvey_Phone]
                            ,[PersonCompletingSurvey_PhoneExt]
                            ,[City]
                            ,[US_StateId]
                            ,[ZipCode]
                            ,[LargestLocationCity]
                            ,[LargestLocationStateId]
                            ,[LargestLocationZipCode]
                            ,[IsControlledGroup]
                            ,[NAICSMasterCodeId]
                            ,[OrganizationTypeId]
                            ,[HasUnionEmployees]
                            ,[HasLeasedStaffedEmployees]
                            ,[IsApplicableLargeEmployer]
                            ,[ActiveEmployeesCount]
                            ,[EligibleEmployeesCount]
                            ,[HasVariableHourEmployees]
                            ,[PartTimeEmployeesCount]
                            ,[FullTimeEmployeeCount]
                            ,[OtherEmployeeCount]
                            ,[AreAncillaryProductsOffered]
                            ,[NumberActivePlansOfferedTypeId]
                            ,[NumberRetireePlansOfferedTypeId]
                            ,[PremiumContributionStrategyTypeId]
                            ,[FullTimeHoursPerWeekTypeId]
                            ,[EligibleWaitingPeriodTypeId]
                            ,[HasOptOutBonus]
                            ,[OptOutBonusAmt_Single]
                            ,[OptOutBonusAmt_Family]
                            ,[DomesticPartnerCoverageTypeId]
                            ,[HasEarlyRetireeCoverageInActivePlans]
                            ,[EarlyRetireePremiumShareTypeId]
                            ,[PresentationInterestTypeId]
                            ,[ErrorCount]
                            ,[CoverSameSexSpouses]
                    FROM [dbo].[SurveyResponse_General] 
                    WHERE ResponseId = @0";
        string SurveyResponse_ActivePlanSql = @"SELECT [ActivePlanId]
                          ,[ResponseId]
                          ,[EnterPlanData]
                          ,[PlanNumber]
                          ,[PlanName]
                          ,[ActivePlanTypeId]
                          ,[IsPlanGrandfathered]
                          ,[IsPlanGrandmothered]
                          ,[HRAIncluded]
                          ,[HSAIncluded]
                          ,[MERPIncluded]
                          ,[HRAHSAMERP_NoneOfTheAbove]
                          ,[FundingMethodTypeId]
                          ,[PremiumTierTypeId]
                          ,[UnitRateTypeId]
                          ,[NumberEnrolled_Single]
                          ,[NumberEnrolled_EmployeePlusOne]
                          ,[NumberEnrolled_EmployeePlusChild]
                          ,[NumberEnrolled_EmployeePlusSpouse]
                          ,[NumberEnrolled_Family]
                          ,[MonthlyPremiumAmt_Single]
                          ,[MonthlyPremiumAmt_EmployeePlusOne]
                          ,[MonthlyPremiumAmt_EmployeePlusChild]
                          ,[MonthlyPremiumAmt_EmployeePlusSpouse]
                          ,[MonthlyPremiumAmt_Family]
                          ,[HasTobaccoSurcharge]
                          ,[TobaccoSurchargePct]
                          ,[ContributionsBasedOnPercentageOfSalary]
                          ,[ContributionBasisTypeId]
                          ,[EmployeeContributionAmt_Single]
                          ,[EmployeeContributionAmt_EmployeePlusOne]
                          ,[EmployeeContributionAmt_EmployeePlusChild]
                          ,[EmployeeContributionAmt_EmployeePlusSpouse]
                          ,[EmployeeContributionAmt_Family]
                          ,[EmployeeContributionPctOfPremium_Single]
                          ,[EmployeeContributionPctOfPremium_EmployeePlusOne]
                          ,[EmployeeContributionPctOfPremium_EmployeePlusChild]
                          ,[EmployeeContributionPctOfPremium_EmployeePlusSpouse]
                          ,[EmployeeContributionPctOfPremium_Family]
                          ,[Employer_HRA_ContributionAmt_Single]
                          ,[Employer_HRA_ContributionAmt_EmployeePlusOne]
                          ,[Employer_HRA_ContributionAmt_EmployeePlusChild]
                          ,[Employer_HRA_ContributionAmt_EmployeePlusSpouse]
                          ,[Employer_HRA_ContributionAmt_Family]
                          ,[Employer_HSA_ContributionAmt_Single]
                          ,[Employer_HSA_ContributionAmt_EmployeePlusOne]
                          ,[Employer_HSA_ContributionAmt_EmployeePlusChild]
                          ,[Employer_HSA_ContributionAmt_EmployeePlusSpouse]
                          ,[Employer_HSA_ContributionAmt_Family]
                          ,[HasSpecificStopLossCoverage]
                          ,[HasAggregateStopLossCoverage]
                          ,[SpecificStopLossLevelAmt]
                          ,[SpecificStopLossPremiumAmt_Single]
                          ,[SpecificStopLossPremiumAmt_EmployeePlusOne]
                          ,[SpecificStopLossPremiumAmt_EmployeePlusChild]
                          ,[SpecificStopLossPremiumAmt_EmployeePlusSpouse]
                          ,[SpecificStopLossPremiumAmt_Family]
                          ,[ProposedIncreasePct]
                          ,[FinalIncreasePct]
                          ,[MedicalExpenseReimbursementRenewalRateTypeId]
                          ,[RenewalDate]
                          ,[CopayAmt_Inpatient_InNetwork]
                          ,[HasCopay_Inpatient_InNetwork]
                          ,[HasCopayPerDay_Inpatient_InNetwork]
                          ,[CopayAmt_Inpatient_OutOfNetwork]
                          ,[HasCopay_Inpatient_OutOfNetwork]
                          ,[HasCopayPerDay_Inpatient_OutOfNetwork]
                          ,[AnnualPlanDeductibleAmt_Single_InNetwork]
                          ,[HasAnnualPlanDeductible_Single_InNetwork]
                          ,[AnnualPlanDeductibleAmt_Single_OutOfNetwork]
                          ,[HasAnnualPlanDeductible_Single_OutOfNetwork]
                          ,[AnnualPlanDeductibleAmt_Family_InNetwork]
                          ,[HasAnnualPlanDeductible_Family_InNetwork]
                          ,[HasAnnualPlanDeductible_xEE_Family_InNetwork]
                          ,[AnnualPlanDeductibleAmt_Family_OutOfNetwork]
                          ,[HasAnnualPlanDeductible_Family_OutOfNetwork]
                          ,[HasAnnualPlanDeductible_xEE_Family_OutOfNetwork]
                          ,[PlanCoinsurancePct_InNetwork]
                          ,[HasPlanCoinsurance_InNetwork]
                          ,[PlanCoinsurancePct_OutOfNetwork]
                          ,[HasPlanCoinsurance_OutOfNetwork]
                          ,[AnnualOutOfPocketMaxAmt_Single_InNetwork]
                          ,[HasAnnualOutOfPocketMax_Single_InNetwork]
                          ,[AnnualOutOfPocketMaxAmt_Single_OutOfNetwork]
                          ,[HasAnnualOutOfPocketMax_Single_OutOfNetwork]
                          ,[AnnualOutOfPocketMaxAmt_Family_InNetwork]
                          ,[HasAnnualOutOfPocketMax_Family_InNetwork]
                          ,[HasAnnualOutOfPocketMax_xEE_Family_InNetwork]
                          ,[AnnualOutOfPocketMaxAmt_Family_OutOfNetwork]
                          ,[HasAnnualOutOfPocketMax_Family_OutOfNetwork]
                          ,[HasAnnualOutOfPocketMax_xEE_Family_OutOfNetwork]
                          ,[HasMinValueCoverage]
                          ,[HasMinEssentialCoverage]
                          ,[PlanDesignMetalLevelTypeId]
                          ,[HasSkinnyPlan]
                          ,[HasERHospitalCoverage]
                          ,[HasPhysicianVisitsCoverage]
                          ,[HasPharmacyCoverage]
                          ,[HasLab_ImagingCoverage]
                          ,[CopayAmt_PCP]
                          ,[HasCopay_PCP]
                          ,[CopayAmt_SCP]
                          ,[HasCopay_SCP]
                          ,[CopayAmt_UrgentCare]
                          ,[HasCopay_UrgentCare]
                          ,[CopayAmt_ER]
                          ,[HasCopay_ER]
                          ,[HRA_PreventiveSvcs]
                          ,[HSA_PreventiveSvcs]
                          ,[InfertilityCoverageTypeId]
                          ,[HasWellnessProgram]
                          ,[WellnessProgramProviderTypeId]
                          ,[HasWellness_HealthRiskAssessment]
                          ,[HasWellness_SeminarsWorkshops]
                          ,[HasWellness_PhysicalExamBloodDraw]
                          ,[HasWellness_Coaching]
                          ,[HasWellness_Incentives]
                          ,[HasWellness_WebPortal]
                          ,[HasWellness_Other]
                          ,[HasWellnessIncentives_PremiumReduction]
                          ,[HasWellnessIncentives_HealthClubDues]
                          ,[HasWellnessIncentives_PaidTimeOff]
                          ,[HasWellnessIncentives_Min]
                          ,[HasWellnessIncentives_Other]
                          ,[HasRxPlan]
                          ,[UpdatedByUserID]
                          ,[SelfFundedDeductible]
                          ,[HasReinsuranceCaptible]
                          ,[AleEmployerStrategy]
                      FROM [dbo].[SurveyResponse_ActivePlan]
                    WHERE ResponseId = @0";
        string SurveyResponse_RxPlanSql = @"SELECT [RxPlanId]
                          ,[ActivePlanId]
                          ,[FundingMethodTypeId]
                          ,[IsCoveredUnderMajorMedical]
                          ,[DoesCoPay_CoinsApplyAfterMajorMedicalDeductible]
                          ,[HasSeparateAnnualDeductibleAppliedToGenericsOnly]
                          ,[DoesSeparateAnnualDeductibleApplyForGeneric]
                          ,[SeparateAnnualDeductibleAmt_Single]
                          ,[HasSeparateAnnualDeductibleAmt_FamilyOnly]
                          ,[SeparateAnnualDeductibleAmt_Family]
                          ,[HasSeparateAnnualDeductibleAmt_xEE]
                          ,[CoPayCoinsuranceStructureTypeId]
                          ,[RxPlanTierCountTypeId]
                          ,[HasSeparateDeductibleForInjectibles]
                          ,[AddedCostForBrandDrugsTypeId]
                          ,[MailOrderPlanDesignTypeId]
                      FROM [dbo].[SurveyResponse_RxPlan]
                      WHERE ActivePlanId = @0";
        string SurveyResponse_RxPlanTierSql = @"SELECT [RxPlanTierId]
                          ,[RxPlanId]
                          ,[TierNumber]
                          ,[RxPlanTypeId]
                          ,[EmployeeCopayAmt]
                          ,[HasEmployeeCopay]
                          ,[EmployeeCoinsurancePctTypeId]
                          ,[HasEmployeeCoinsurance]
                          ,[MaxCoinsuranceAmt]
                      FROM [dbo].[SurveyResponse_RxPlanTier]
                      WHERE RxPlanId = @0";
        string SurveyResponse_RetireePlanSql = @"SELECT [RetireePlanId]
                          ,[ResponseId]
                          ,[PlanNumber]
                          ,[EnterPlanData]
                          ,[RetireePlanTypeId]
                          ,[FundingMethodTypeId]
                          ,[RetireePlanArrangementTypeId]
                          ,[RetireePlanOfferedToTypeId]
                          ,[MonthlyPremiumAmt_Single]
                          ,[MonthlyPremiumAmt_Family]
                          ,[IsMonthlyPremiumRetireeOnly]
                          ,[IsEmployerContribBasedOnAgeOrYearsOfSvc]
                          ,[MonthlyEmployerContribAmt_Single]
                          ,[MonthlyEmployerContribAmt_Family]
                          ,[ProposedPremiumChangePct]
                          ,[FinalPremiumChangePct]
                          ,[HasRxPlan]
                          ,[HasMedicarePartDReimbursment]
                          ,[UpdatedByUserID]
                      FROM [dbo].[SurveyResponse_RetireePlan]
                      WHERE ResponseId = @0";
        string SurveyResponse_Section125Sql = @"SELECT [Section125Id]
                          ,[ResponseId]
                          ,[Offers_FSA_TraditionalMedical]
                          ,[Offers_FSA_LimitedPurposeMedical]
                          ,[Offers_FSA_DependentCare]
                          ,[Offers_FSA_PremiumOnlyPlan]
                          ,[Offers_FSA_NoneOfTheAbove]
                          ,[MaxAnnualContribution_TraditionalMedical_FSA]
                          ,[EmployeesEnrolledCount_TraditionalMedical_FSA]
                          ,[AvgAnnualContribution_TraditionalMedical_FSA]
                          ,[MaxAnnualContribution_LtdPurposeMedical_FSA]
                          ,[EmployeesEnrolledCount_LtdPurposeMedical_FSA]
                          ,[AvgAnnualContribution_LtdPurposeMedical_FSA]
                          ,[MaxAnnualContribution_DependentCare_FSA]
                          ,[EmployeesEnrolledCount_DependentCare_FSA]
                          ,[AvgAnnualContribution_DependentCare_FSA]
                      FROM [dbo].[SurveyResponse_Section125]
                      WHERE ResponseId = @0";

        #endregion

        #region SurveyResponse Data
        //Get the paginated list of surveys for a partner firm.
        public IEnumerable<SurveySummaryModel> GetSurveyListData()
        {
            //TODO
            //TODO:  convert this to use pagination
            IEnumerable<SurveySummaryModel> t;
            using (hpsDB db = new hpsDB())
            {
                var result = db.Fetch<SurveySummaryModel>(SurveyListSql);
                t = result;
            }
            return t;

        }

        //Get the SurveyResponseGeneral data.
        public SurveySummaryModel GetSurveyResponse_Summary(int responseId)
        {
            SurveySummaryModel t;
            using (hpsDB db = new hpsDB())
            {
                var result = db.SingleOrDefault<SurveySummaryModel>(SurveySummaryModelSql, responseId);
                t = result;
            }
            return t;

        }

        public SurveyResponse_General GetSurveyResponse_General(int responseId)
        {
            SurveyResponse_General t;
            using (hpsDB db = new hpsDB())
            {
                var result = db.SingleOrDefault<SurveyResponse_General>(
                    @"SELECT [ResponseId]
                            ,[SurveyId]
                            ,[WebID]
                            ,[MemberFirmId]
                            ,[ClientId]
                            ,[NotParticipatingReasonTypeId]
                            ,[SurveyResponseStatusTypeId]
                            ,[CreatedOn]
                            ,[CreatedBy]
                            ,[UpdatedOn]
                            ,[UpdatedBy]
                            ,[CompletedBy]
                            ,[CompletedOn]
                            ,[OrganizationName]
                            ,[PersonCompletingSurvey]
                            ,[PersonCompletingSurvey_Title]
                            ,[PersonCompletingSurvey_Email]
                            ,[PersonCompletingSurvey_Phone]
                            ,[PersonCompletingSurvey_PhoneExt]
                            ,[City]
                            ,[US_StateId]
                            ,[ZipCode]
                            ,[LargestLocationCity]
                            ,[LargestLocationStateId]
                            ,[LargestLocationZipCode]
                            ,[IsControlledGroup]
                            ,[NAICSMasterCodeId]
                            ,[OrganizationTypeId]
                            ,[HasUnionEmployees]
                            ,[HasLeasedStaffedEmployees]
                            ,[IsApplicableLargeEmployer]
                            ,[ActiveEmployeesCount]
                            ,[EligibleEmployeesCount]
                            ,[HasVariableHourEmployees]
                            ,[PartTimeEmployeesCount]
                            ,[FullTimeEmployeeCount]
                            ,[OtherEmployeeCount]
                            ,[AreAncillaryProductsOffered]
                            ,[NumberActivePlansOfferedTypeId]
                            ,[NumberRetireePlansOfferedTypeId]
                            ,[PremiumContributionStrategyTypeId]
                            ,[FullTimeHoursPerWeekTypeId]
                            ,[EligibleWaitingPeriodTypeId]
                            ,[HasOptOutBonus]
                            ,[OptOutBonusAmt_Single]
                            ,[OptOutBonusAmt_Family]
                            ,[DomesticPartnerCoverageTypeId]
                            ,[HasEarlyRetireeCoverageInActivePlans]
                            ,[EarlyRetireePremiumShareTypeId]
                            ,[PresentationInterestTypeId]
                            ,[ErrorCount]
                            ,[CoverSameSexSpouses]
                    FROM [dbo].[SurveyResponse_General] 
                    WHERE ResponseId = @0", responseId);
                t = result;
            }
            return t;
        }

        public SurveyResponseItem GetSurvey(int responseId)
        {
            SurveyResponseItem item = new SurveyResponseItem();
            using (hpsDB db = new hpsDB())
            {
                // get general response
                var general = db.SingleOrDefault<SurveyResponse_General>(SurveyResponseItemSql, responseId);
                item.GeneralResponse = general;

                // get active plans with RxPlan
                var responseActivePlans = db.Fetch<SurveyResponse_ActivePlan>(SurveyResponse_ActivePlanSql, responseId);
                foreach (SurveyResponse_ActivePlan plan in responseActivePlans)
                {
                    // get RxPlan for each active plan
                    var rxPlan = db.SingleOrDefault<SurveyResponse_RxPlan>(SurveyResponse_RxPlanSql, plan.ActivePlanId);
                    var rxTiers = db.Fetch<SurveyResponse_RxPlanTier>(SurveyResponse_RxPlanTierSql, plan.ActivePlanId);
                    var surveyRxPlan = new SurveyRxPlan(rxPlan, rxTiers);
                    // create new SurveyActivePlan(plan, rxPlan) 
                    var surveyActivePlan = new SurveyActivePlan(plan, surveyRxPlan);
                    item.ActivePlans.Add(surveyActivePlan);
                }

                // get retiree plan
                var retiree = db.SingleOrDefault<SurveyResponse_RetireePlan>(SurveyResponse_RetireePlanSql, responseId);
                item.RetireePlan = retiree;

                // get section125 
                var section125 = db.SingleOrDefault<SurveyResponse_Section125>(SurveyResponse_Section125Sql, responseId);

            }
            return item;
        }

        public void SaveSurveySummaryData(SurveySummaryModel survey)
        {

        }
        #endregion
    }
}
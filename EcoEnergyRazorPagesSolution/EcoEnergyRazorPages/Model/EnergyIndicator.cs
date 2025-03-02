namespace EcoEnergyRazorPages.Model
{
    public class EnergyIndicator : IComparable<EnergyIndicator>
    {
        public DateOnly Data { get; set; }
        public float PBEE_Hidroelectr { get; set; }
        public float PBEE_Carbo { get; set; }
        public float PBEE_GasNat { get; set; }
        public float PBEE_FuelOil { get; set; }
        public float PBEE_CiclComb { get; set; }
        public float PBEE_Nuclear { get; set; }
        public float CDEEBC_ProdBruta { get; set; }
        public float CDEEBC_ConsumAux { get; set; }
        public float CDEEBC_ProdNeta { get; set; }
        public float CDEEBC_ConsumBomb { get; set; }
        public float CDEEBC_ProdDisp { get; set; }
        public float CDEEBC_TotVendesXarxaCentral { get; set; }
        public float CDEEBC_SaldoIntercanviElectr { get; set; }
        public float CDEEBC_DemandaElectr { get; set; }
        public string? CDEEBC_TotalEBCMercatRegulat { get; set; }
        public string? CDEEBC_TotalEBCMercatLliure { get; set; }
        public float? FEE_Industria { get; set; }
        public float? FEE_Terciari { get; set; }
        public float? FEE_Domestic { get; set; }
        public float? FEE_Primari { get; set; }
        public float? FEE_Energetic { get; set; }
        public float? FEEI_ConsObrPub { get; set; }
        public float? FEEI_SiderFoneria { get; set; }
        public float? FEEI_Metalurgia { get; set; }
        public float? FEEI_IndusVidre { get; set; }
        public float? FEEI_CimentsCalGuix { get; set; }
        public float? FEEI_AltresMatConstr { get; set; }
        public float? FEEI_QuimPetroquim { get; set; }
        public float? FEEI_ConstrMedTrans { get; set; }
        public float? FEEI_RestaTransforMetal { get; set; }
        public float? FEEI_AlimBegudaTabac { get; set; }
        public float? FEEI_TextilConfecCuirCalçat { get; set; }
        public float? FEEI_PastaPaperCartro { get; set; }
        public float? FEEI_AltresIndus { get; set; }
        public float DGGN_PuntFrontEnagas { get; set; }
        public float DGGN_DistrAlimGNL { get; set; }
        public float DGGN_ConsumGNCentrTerm { get; set; }
        public float CCAC_GasolinaAuto { get; set; }
        public float CCAC_GasoilA { get; set; }

        public int CompareTo(EnergyIndicator? other)
        {
            if (other == null) return 1;
            return this.Data.CompareTo(other.Data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace ImportadorDNE
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // LOG_BAIRRO.txt
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_BAIRRO.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                foreach (string line in lines)
                {
                    String Linha = line;
                    String BaiNoAbrev = String.Empty;
                    String BaiNo = String.Empty;
                    String LocNu = String.Empty;
                    String UFeSg = String.Empty;
                    String BaiNu = String.Empty;

                    Int32 DelimitadorBaiNoAbrev = Linha.LastIndexOf('@');
                    if (DelimitadorBaiNoAbrev > 0)
                    {
                        BaiNoAbrev = Linha.Substring(DelimitadorBaiNoAbrev + 1, Linha.Length - DelimitadorBaiNoAbrev - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorBaiNoAbrev).Trim();
                    }
                    Int32 DelimitadorBaiNo = Linha.LastIndexOf('@');
                    if (DelimitadorBaiNo > 0)
                    {
                        BaiNo = Linha.Substring(DelimitadorBaiNo + 1, Linha.Length - DelimitadorBaiNo - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorBaiNo).Trim();
                    }
                    Int32 DelimitadorLocNu = Linha.LastIndexOf('@');
                    if (DelimitadorLocNu > 0)
                    {
                        LocNu = Linha.Substring(DelimitadorLocNu + 1, Linha.Length - DelimitadorLocNu - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLocNu).Trim();
                    }
                    Int32 DelimitadorUFeSg = Linha.LastIndexOf('@');
                    if (DelimitadorUFeSg > 0)
                    {
                        UFeSg = Linha.Substring(DelimitadorUFeSg + 1, Linha.Length - DelimitadorUFeSg - 1).Trim();
                        BaiNu = Linha.Substring(0, DelimitadorUFeSg).Trim();
                    }

                    Int32? IdBaiNu = null;
                    Int32? CodLocNu = null;

                    if (!String.IsNullOrEmpty(BaiNu))
                        IdBaiNu = Convert.ToInt32(BaiNu);

                    if (!String.IsNullOrEmpty(LocNu))
                        CodLocNu = Convert.ToInt32(LocNu);

                    var recLogBairro = context.Log_Bairro.FirstOrDefault(LB => LB.BAI_NU == IdBaiNu.Value);
                    if (recLogBairro == null)
                    {
                        var LogBairro = new Log_Bairro
                        {
                            BAI_NU = IdBaiNu.Value,
                            UFE_SG = UFeSg,
                            LOC_NU = CodLocNu.Value,
                            BAI_NO = BaiNo,
                            BAI_NO_ABREV = BaiNoAbrev,
                        };

                        context.Log_Bairro.Add(LogBairro);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogBairro.UFE_SG = UFeSg;
                        recLogBairro.LOC_NU = CodLocNu.Value;
                        recLogBairro.BAI_NO = BaiNo;
                        recLogBairro.BAI_NO_ABREV = BaiNoAbrev;
                        context.SaveChanges();
                    }
                }
            }
        }

        // Log_Faixa_UF.txt
        protected void Button2_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\Log_Faixa_UF.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Faixa_UF;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String UFECEPFIM = String.Empty;
                    String UFECEPINI = String.Empty;
                    String UFESG = String.Empty;

                    Int32 DelimitadorUFECEPFIM = Linha.LastIndexOf('@');
                    if (DelimitadorUFECEPFIM > 0)
                    {
                        UFECEPFIM = Linha.Substring(DelimitadorUFECEPFIM + 1, Linha.Length - DelimitadorUFECEPFIM - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUFECEPFIM).Trim();
                    }
                    Int32 DelimitadorUFECEPINI = Linha.LastIndexOf('@');
                    if (DelimitadorUFECEPINI > 0)
                    {
                        UFECEPINI = Linha.Substring(DelimitadorUFECEPINI + 1, Linha.Length - DelimitadorUFECEPINI - 1).Trim();
                        UFESG = Linha.Substring(0, DelimitadorUFECEPINI).Trim();
                    }

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.UFE_SG.Equals(UFESG) && LB.UFE_CEP_INI.Equals(UFECEPINI));
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Faixa_UF
                        {
                            UFE_SG = UFESG,
                            UFE_CEP_INI = UFECEPINI,
                            UFE_CEP_FIM = UFECEPFIM,
                        };

                        context.Log_Faixa_UF.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.UFE_SG = UFESG;
                        recLogFaixaUF.UFE_CEP_INI = UFECEPINI;
                        recLogFaixaUF.UFE_CEP_FIM = UFECEPFIM;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_LOCALIDADE.txt
        protected void Button3_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOCALIDADE.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Localidade;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String MUNNU = String.Empty;
                    String LOCNOABREV = String.Empty;
                    String LOCNUSUB = String.Empty;
                    String LOCINTIPOLOC = String.Empty;
                    String LOCINSIT = String.Empty;
                    String _CEP = String.Empty;
                    String LOCNO = String.Empty;
                    String UFESG = String.Empty;
                    String LOCNU = String.Empty;

                    Int32 DelimitadorMUNNU = Linha.LastIndexOf('@');
                    if (DelimitadorMUNNU > 0)
                    {
                        MUNNU = Linha.Substring(DelimitadorMUNNU + 1, Linha.Length - DelimitadorMUNNU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorMUNNU).Trim();
                    }

                    Int32 DelimitadorLOCNOABREV = Linha.LastIndexOf('@');
                    if (DelimitadorLOCNOABREV > 0)
                    {
                        LOCNOABREV = Linha.Substring(DelimitadorLOCNOABREV + 1, Linha.Length - DelimitadorLOCNOABREV - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCNOABREV).Trim();
                    }

                    Int32 DelimitadorLOCNUSUB = Linha.LastIndexOf('@');
                    if (DelimitadorLOCNUSUB > 0)
                    {
                        LOCNUSUB = Linha.Substring(DelimitadorLOCNUSUB + 1, Linha.Length - DelimitadorLOCNUSUB - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCNUSUB).Trim();
                    }

                    Int32 DelimitadorLOCINTIPOLOC = Linha.LastIndexOf('@');
                    if (DelimitadorLOCINTIPOLOC > 0)
                    {
                        LOCINTIPOLOC = Linha.Substring(DelimitadorLOCINTIPOLOC + 1, Linha.Length - DelimitadorLOCINTIPOLOC - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCINTIPOLOC).Trim();
                    }

                    Int32 DelimitadorLOCINSIT = Linha.LastIndexOf('@');
                    if (DelimitadorLOCINSIT > 0)
                    {
                        LOCINSIT = Linha.Substring(DelimitadorLOCINSIT + 1, Linha.Length - DelimitadorLOCINSIT - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCINSIT).Trim();
                    }

                    Int32 DelimitadorCEP = Linha.LastIndexOf('@');
                    if (DelimitadorCEP > 0)
                    {
                        _CEP = Linha.Substring(DelimitadorCEP + 1, Linha.Length - DelimitadorCEP - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorCEP).Trim();
                    }

                    Int32 DelimitadorLOCNO = Linha.LastIndexOf('@');
                    if (DelimitadorLOCNO > 0)
                    {
                        LOCNO = Linha.Substring(DelimitadorLOCNO + 1, Linha.Length - DelimitadorLOCNO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCNO).Trim();
                    }
                    Int32 DelimitadorUFESG = Linha.LastIndexOf('@');
                    if (DelimitadorUFESG > 0)
                    {
                        UFESG = Linha.Substring(DelimitadorUFESG + 1, Linha.Length - DelimitadorUFESG - 1).Trim();
                        LOCNU = Linha.Substring(0, DelimitadorUFESG).Trim();
                    }

                    Int32? IdLOCNU = null;
                    if (!String.IsNullOrEmpty(LOCNU))
                        IdLOCNU = Convert.ToInt32(LOCNU);

                    Int32? CodLOCNUSUB = null;
                    if (!String.IsNullOrEmpty(LOCNUSUB))
                        CodLOCNUSUB = Convert.ToInt32(LOCNUSUB);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.LOC_NU == IdLOCNU.Value);
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Localidade
                        {
                            LOC_NU = IdLOCNU.Value,
                            UFE_SG = UFESG,
                            LOC_NO = LOCNO,
                            CEP = _CEP,
                            LOC_IN_SIT = LOCINSIT,
                            LOC_IN_TIPO_LOC = LOCINTIPOLOC,
                            LOC_NU_SUB = CodLOCNUSUB,
                            LOC_NO_ABREV = LOCNOABREV,
                            MUN_NU = MUNNU,
                        };

                        context.Log_Localidade.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.LOC_NU = IdLOCNU.Value;
                        recLogFaixaUF.UFE_SG = UFESG;
                        recLogFaixaUF.LOC_NO = LOCNO;
                        recLogFaixaUF.CEP = _CEP;
                        recLogFaixaUF.LOC_IN_SIT = LOCINSIT;
                        recLogFaixaUF.LOC_IN_TIPO_LOC = LOCINTIPOLOC;
                        recLogFaixaUF.LOC_NU_SUB = CodLOCNUSUB;
                        recLogFaixaUF.LOC_NO_ABREV = LOCNOABREV;
                        recLogFaixaUF.MUN_NU = MUNNU;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_VAR_LOC.txt
        protected void Button4_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_VAR_LOC.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Var_Loc;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String VALTX = String.Empty;
                    String VALNU = String.Empty;
                    String LOCNU = String.Empty;

                    Int32 DelimitadorUFECEPFIM = Linha.LastIndexOf('@');
                    if (DelimitadorUFECEPFIM > 0)
                    {
                        VALTX = Linha.Substring(DelimitadorUFECEPFIM + 1, Linha.Length - DelimitadorUFECEPFIM - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUFECEPFIM).Trim();
                    }
                    Int32 DelimitadorUFECEPINI = Linha.LastIndexOf('@');
                    if (DelimitadorUFECEPINI > 0)
                    {
                        VALNU = Linha.Substring(DelimitadorUFECEPINI + 1, Linha.Length - DelimitadorUFECEPINI - 1).Trim();
                        LOCNU = Linha.Substring(0, DelimitadorUFECEPINI).Trim();
                    }

                    Int32? IdLOCNU = null;
                    if (!String.IsNullOrEmpty(LOCNU))
                        IdLOCNU = Convert.ToInt32(LOCNU);

                    Int32? CodVALNU = null;
                    if (!String.IsNullOrEmpty(VALNU))
                        CodVALNU = Convert.ToInt32(VALNU);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.LOC_NU == IdLOCNU.Value && LB.VAL_NU == CodVALNU);
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Var_Loc
                        {
                            LOC_NU = IdLOCNU.Value,
                            VAL_NU = CodVALNU.Value,
                            VAL_TX = VALTX,
                        };

                        context.Log_Var_Loc.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.LOC_NU = IdLOCNU.Value;
                        recLogFaixaUF.VAL_NU = CodVALNU.Value;
                        recLogFaixaUF.VAL_TX = VALTX;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_FAIXA_LOCALIDADE
        protected void Button5_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_FAIXA_LOCALIDADE.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Faixa_Localidade;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String LOCCEPFIM = String.Empty;
                    String LOCCEPINI = String.Empty;
                    String LOCNU = String.Empty;

                    Int32 DelimitadorUFECEPFIM = Linha.LastIndexOf('@');
                    if (DelimitadorUFECEPFIM > 0)
                    {
                        LOCCEPFIM = Linha.Substring(DelimitadorUFECEPFIM + 1, Linha.Length - DelimitadorUFECEPFIM - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUFECEPFIM).Trim();
                    }
                    Int32 DelimitadorUFECEPINI = Linha.LastIndexOf('@');
                    if (DelimitadorUFECEPINI > 0)
                    {
                        LOCCEPINI = Linha.Substring(DelimitadorUFECEPINI + 1, Linha.Length - DelimitadorUFECEPINI - 1).Trim();
                        LOCNU = Linha.Substring(0, DelimitadorUFECEPINI).Trim();
                    }

                    Int32? IdLOCNU = null;
                    if (!String.IsNullOrEmpty(LOCNU))
                        IdLOCNU = Convert.ToInt32(LOCNU);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.LOC_NU == IdLOCNU.Value && LB.LOC_CEP_INI.Equals(LOCCEPINI));
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Faixa_Localidade
                        {
                            LOC_NU = IdLOCNU.Value,
                            LOC_CEP_INI = LOCCEPINI,
                            LOC_CEP_FIM = LOCCEPFIM,
                        };

                        context.Log_Faixa_Localidade.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.LOC_NU = IdLOCNU.Value;
                        recLogFaixaUF.LOC_CEP_INI = LOCCEPINI;
                        recLogFaixaUF.LOC_CEP_FIM = LOCCEPFIM;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_VAR_BAI
        protected void Button6_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_VAR_BAI.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Var_Bai;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String VDBTX = String.Empty;
                    String VDBNU = String.Empty;
                    String BAINU = String.Empty;

                    Int32 DelimitadorVDBTX = Linha.LastIndexOf('@');
                    if (DelimitadorVDBTX > 0)
                    {
                        VDBTX = Linha.Substring(DelimitadorVDBTX + 1, Linha.Length - DelimitadorVDBTX - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorVDBTX).Trim();
                    }
                    Int32 DelimitadorBAINU = Linha.LastIndexOf('@');
                    if (DelimitadorBAINU > 0)
                    {
                        VDBNU = Linha.Substring(DelimitadorBAINU + 1, Linha.Length - DelimitadorBAINU - 1).Trim();
                        BAINU = Linha.Substring(0, DelimitadorBAINU).Trim();
                    }

                    Int32? IdBAINU = null;
                    if (!String.IsNullOrEmpty(BAINU))
                        IdBAINU = Convert.ToInt32(BAINU);

                    Int32? CodVDBNU = null;
                    if (!String.IsNullOrEmpty(VDBNU))
                        CodVDBNU = Convert.ToInt32(VDBNU);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.BAI_NU == IdBAINU.Value && LB.VDB_NU == CodVDBNU.Value);
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Var_Bai
                        {
                            BAI_NU = IdBAINU.Value,
                            VDB_NU = CodVDBNU.Value,
                            VDB_TX = VDBTX,
                        };

                        context.Log_Var_Bai.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.BAI_NU = IdBAINU.Value;
                        recLogFaixaUF.VDB_NU = CodVDBNU.Value;
                        recLogFaixaUF.VDB_TX = VDBTX;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_FAIXA_BAIRRO
        protected void Button7_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_FAIXA_BAIRRO.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Faixa_Bairro;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String FCBCEPFIM = String.Empty;
                    String FCBCEPINI = String.Empty;
                    String BAINU = String.Empty;

                    Int32 DelimitadorFCBCEPFIM = Linha.LastIndexOf('@');
                    if (DelimitadorFCBCEPFIM > 0)
                    {
                        FCBCEPFIM = Linha.Substring(DelimitadorFCBCEPFIM + 1, Linha.Length - DelimitadorFCBCEPFIM - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorFCBCEPFIM).Trim();
                    }
                    Int32 DelimitadorFCBCEPINI = Linha.LastIndexOf('@');
                    if (DelimitadorFCBCEPINI > 0)
                    {
                        FCBCEPINI = Linha.Substring(DelimitadorFCBCEPINI + 1, Linha.Length - DelimitadorFCBCEPINI - 1).Trim();
                        BAINU = Linha.Substring(0, DelimitadorFCBCEPINI).Trim();
                    }

                    Int32? IdBAINU = null;
                    if (!String.IsNullOrEmpty(BAINU))
                        IdBAINU = Convert.ToInt32(BAINU);

                    var recLogFaixaBairro = recLog.FirstOrDefault(LB => LB.BAI_NU == IdBAINU.Value && LB.FCB_CEP_INI.Equals(FCBCEPINI));
                    if (recLogFaixaBairro == null)
                    {
                        var LogFaixaBairro = new Log_Faixa_Bairro
                        {
                            BAI_NU = IdBAINU.Value,
                            FCB_CEP_INI = FCBCEPINI,
                            FCB_CEP_FIM = FCBCEPFIM,
                        };

                        context.Log_Faixa_Bairro.Add(LogFaixaBairro);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaBairro.BAI_NU = IdBAINU.Value;
                        recLogFaixaBairro.FCB_CEP_INI = FCBCEPINI;
                        recLogFaixaBairro.FCB_CEP_FIM = FCBCEPFIM;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_CPC
        protected void Button8_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_CPC.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_CPC;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String _CEP = String.Empty;
                    String CPCENDERECO = String.Empty;
                    String CPCNO = String.Empty;
                    String LocNu = String.Empty;
                    String UFeSg = String.Empty;
                    String CPCNU = String.Empty;

                    Int32 DelimitadorCEP = Linha.LastIndexOf('@');
                    if (DelimitadorCEP > 0)
                    {
                        _CEP = Linha.Substring(DelimitadorCEP + 1, Linha.Length - DelimitadorCEP - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorCEP).Trim();
                    }
                    Int32 DelimitadorCPCENDERECO = Linha.LastIndexOf('@');
                    if (DelimitadorCPCENDERECO > 0)
                    {
                        CPCENDERECO = Linha.Substring(DelimitadorCPCENDERECO + 1, Linha.Length - DelimitadorCPCENDERECO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorCPCENDERECO).Trim();
                    }
                    Int32 DelimitadorCPCNO = Linha.LastIndexOf('@');
                    if (DelimitadorCPCNO > 0)
                    {
                        CPCNO = Linha.Substring(DelimitadorCPCNO + 1, Linha.Length - DelimitadorCPCNO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorCPCNO).Trim();
                    }
                    Int32 DelimitadorLocNu = Linha.LastIndexOf('@');
                    if (DelimitadorLocNu > 0)
                    {
                        LocNu = Linha.Substring(DelimitadorLocNu + 1, Linha.Length - DelimitadorLocNu - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLocNu).Trim();
                    }
                    Int32 DelimitadorUFeSg = Linha.LastIndexOf('@');
                    if (DelimitadorUFeSg > 0)
                    {
                        UFeSg = Linha.Substring(DelimitadorUFeSg + 1, Linha.Length - DelimitadorUFeSg - 1).Trim();
                        CPCNU = Linha.Substring(0, DelimitadorUFeSg).Trim();
                    }

                    Int32? IdCPCNU = null;
                    Int32? CodLocNu = null;

                    if (!String.IsNullOrEmpty(CPCNU))
                        IdCPCNU = Convert.ToInt32(CPCNU);

                    if (!String.IsNullOrEmpty(LocNu))
                        CodLocNu = Convert.ToInt32(LocNu);

                    var recLogCPC = recLog.FirstOrDefault(LB => LB.CPC_NU == IdCPCNU.Value);
                    if (recLogCPC == null)
                    {
                        var LogCPC = new Log_CPC
                        {
                            CPC_NU = IdCPCNU.Value,
                            UFE_SG = UFeSg,
                            LOC_NU = CodLocNu.Value,
                            CPC_NO = CPCNO,
                            CPC_ENDERECO = CPCENDERECO,
                            CEP = _CEP,
                        };

                        context.Log_CPC.Add(LogCPC);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogCPC.UFE_SG = UFeSg;
                        recLogCPC.LOC_NU = CodLocNu.Value;
                        recLogCPC.CPC_NO = CPCNO;
                        recLogCPC.CPC_ENDERECO = CPCENDERECO;
                        recLogCPC.CEP = _CEP;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_FAIXA_CPC
        protected void Button9_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_FAIXA_CPC.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Faixa_CPC;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String CPCFINAL = String.Empty;
                    String CPCINICIAL = String.Empty;
                    String CPCNU = String.Empty;

                    Int32 DelimitadorCPCFINAL = Linha.LastIndexOf('@');
                    if (DelimitadorCPCFINAL > 0)
                    {
                        CPCFINAL = Linha.Substring(DelimitadorCPCFINAL + 1, Linha.Length - DelimitadorCPCFINAL - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorCPCFINAL).Trim();
                    }
                    Int32 DelimitadorCPCINICIAL = Linha.LastIndexOf('@');
                    if (DelimitadorCPCINICIAL > 0)
                    {
                        CPCINICIAL = Linha.Substring(DelimitadorCPCINICIAL + 1, Linha.Length - DelimitadorCPCINICIAL - 1).Trim();
                        CPCNU = Linha.Substring(0, DelimitadorCPCINICIAL).Trim();
                    }

                    Int32? IdCPCNU = null;
                    if (!String.IsNullOrEmpty(CPCNU))
                        IdCPCNU = Convert.ToInt32(CPCNU);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.CPC_NU == IdCPCNU.Value && LB.CPC_INICIAL.Equals(CPCINICIAL));
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Faixa_CPC
                        {
                            CPC_NU = IdCPCNU.Value,
                            CPC_INICIAL = CPCINICIAL,
                            CPC_FINAL = CPCFINAL,
                        };

                        context.Log_Faixa_CPC.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.CPC_NU = IdCPCNU.Value;
                        recLogFaixaUF.CPC_INICIAL = CPCINICIAL;
                        recLogFaixaUF.CPC_FINAL = CPCFINAL;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_LOGRADOURO_XX Todos
        protected void Button10_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines_AC = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_AC.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_AC);

            string[] lines_AL = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_AL.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_AL);

            string[] lines_AM = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_AM.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_AM);

            string[] lines_AP = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_AP.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_AP);

            string[] lines_BA = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_BA.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_BA);

            string[] lines_CE = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_CE.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_CE);

            string[] lines_DF = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_DF.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_DF);

            string[] lines_ES = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_ES.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_ES);

            string[] lines_GO = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_GO.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_GO);

            string[] lines_MA = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_MA.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_MA);

            string[] lines_MG = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_MG.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_MG);

            string[] lines_MS = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_MS.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_MS);

            string[] lines_MT = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_MT.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_MT);

            string[] lines_PA = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_PA.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_PA);

            string[] lines_PB = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_PB.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_PB);

            string[] lines_PE = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_PE.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_PE);

            string[] lines_PI = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_PI.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_PI);

            string[] lines_PR = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_PR.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_PR);

            string[] lines_RJ = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_RJ.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_RJ);

            string[] lines_RN = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_RN.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_RN);

            string[] lines_RO = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_RO.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_RO);

            string[] lines_RR = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_RR.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_RR);

            string[] lines_RS = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_RS.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_RS);

            string[] lines_SC = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_SC.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_SC);

            string[] lines_SE = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_SE.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_SE);

            string[] lines_SP = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_SP.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_SP);

            string[] lines_TO = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_LOGRADOURO_TO.txt", System.Text.Encoding.UTF8);
            InsertLog_Logradouro(lines_TO);

        }

        private static void InsertLog_Logradouro(string[] lines)
        {
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Logradouro;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String LOGNOABREV = String.Empty;
                    String LOGSTATLO = String.Empty;
                    String TLOTX = String.Empty;
                    String _CEP = String.Empty;
                    String LOGCOMPLEMENTO = String.Empty;
                    String LOGNO = String.Empty;
                    String BAINUFIM = String.Empty;
                    String BAINUINI = String.Empty;
                    String LOCNU = String.Empty;
                    String UFeSg = String.Empty;
                    String LOGNU = String.Empty;

                    Int32 DelimitadorLOGNOABREV = Linha.LastIndexOf('@');
                    if (DelimitadorLOGNOABREV > 0)
                    {
                        LOGNOABREV = Linha.Substring(DelimitadorLOGNOABREV + 1, Linha.Length - DelimitadorLOGNOABREV - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOGNOABREV).Trim();
                    }
                    Int32 DelimitadorLOGSTATLO = Linha.LastIndexOf('@');
                    if (DelimitadorLOGSTATLO > 0)
                    {
                        LOGSTATLO = Linha.Substring(DelimitadorLOGSTATLO + 1, Linha.Length - DelimitadorLOGSTATLO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOGSTATLO).Trim();
                    }
                    Int32 DelimitadorTLOTX = Linha.LastIndexOf('@');
                    if (DelimitadorTLOTX > 0)
                    {
                        TLOTX = Linha.Substring(DelimitadorTLOTX + 1, Linha.Length - DelimitadorTLOTX - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorTLOTX).Trim();
                    }
                    Int32 Delimitador_CEP = Linha.LastIndexOf('@');
                    if (Delimitador_CEP > 0)
                    {
                        _CEP = Linha.Substring(Delimitador_CEP + 1, Linha.Length - Delimitador_CEP - 1).Trim();
                        Linha = Linha.Substring(0, Delimitador_CEP).Trim();
                    }
                    Int32 DelimitadorLOGCOMPLEMENTO = Linha.LastIndexOf('@');
                    if (DelimitadorLOGCOMPLEMENTO > 0)
                    {
                        LOGCOMPLEMENTO = Linha.Substring(DelimitadorLOGCOMPLEMENTO + 1, Linha.Length - DelimitadorLOGCOMPLEMENTO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOGCOMPLEMENTO).Trim();
                    }
                    Int32 DelimitadorLOGNO = Linha.LastIndexOf('@');
                    if (DelimitadorLOGNO > 0)
                    {
                        LOGNO = Linha.Substring(DelimitadorLOGNO + 1, Linha.Length - DelimitadorLOGNO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOGNO).Trim();
                    }
                    Int32 DelimitadorBAINUFIM = Linha.LastIndexOf('@');
                    if (DelimitadorBAINUFIM > 0)
                    {
                        BAINUFIM = Linha.Substring(DelimitadorBAINUFIM + 1, Linha.Length - DelimitadorBAINUFIM - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorBAINUFIM).Trim();
                    }
                    Int32 DelimitadorBAINUINI = Linha.LastIndexOf('@');
                    if (DelimitadorBAINUINI > 0)
                    {
                        BAINUINI = Linha.Substring(DelimitadorBAINUINI + 1, Linha.Length - DelimitadorBAINUINI - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorBAINUINI).Trim();
                    }
                    Int32 DelimitadorLOCNU = Linha.LastIndexOf('@');
                    if (DelimitadorLOCNU > 0)
                    {
                        LOCNU = Linha.Substring(DelimitadorLOCNU + 1, Linha.Length - DelimitadorLOCNU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCNU).Trim();
                    }
                    Int32 DelimitadorUFeSg = Linha.LastIndexOf('@');
                    if (DelimitadorUFeSg > 0)
                    {
                        UFeSg = Linha.Substring(DelimitadorUFeSg + 1, Linha.Length - DelimitadorUFeSg - 1).Trim();
                        LOGNU = Linha.Substring(0, DelimitadorUFeSg).Trim();
                    }

                    Int32? IdLOGNU = null;
                    Int32? CodLocNu = null;
                    Int32? CodBAINUINI = null;
                    Int32? CodBAINUFIM = null;

                    if (!String.IsNullOrEmpty(LOGNU))
                        IdLOGNU = Convert.ToInt32(LOGNU);

                    if (!String.IsNullOrEmpty(LOCNU))
                        CodLocNu = Convert.ToInt32(LOCNU);

                    if (!String.IsNullOrEmpty(BAINUINI))
                        CodBAINUINI = Convert.ToInt32(BAINUINI);

                    if (!String.IsNullOrEmpty(BAINUFIM))
                        CodBAINUFIM = Convert.ToInt32(BAINUFIM);

                    var recLogLogradouro = recLog.FirstOrDefault(LB => LB.LOG_NU == IdLOGNU.Value);
                    if (recLogLogradouro == null)
                    {
                        var LogLogradouro = new Log_Logradouro
                        {
                            LOG_NU = IdLOGNU.Value,
                            UFE_SG = UFeSg,
                            LOC_NU = CodLocNu.Value,
                            BAI_NU_INI = CodBAINUINI.Value,
                            BAI_NU_FIM = CodBAINUFIM.HasValue ? CodBAINUFIM.Value : CodBAINUFIM,
                            LOG_NO = LOGNO,
                            LOG_COMPLEMENTO = LOGCOMPLEMENTO,
                            CEP = _CEP,
                            TLO_TX = TLOTX,
                            LOG_STA_TLO = LOGSTATLO,
                            LOG_NO_ABREV = LOGNOABREV,
                        };

                        context.Log_Logradouro.Add(LogLogradouro);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogLogradouro.LOG_NU = IdLOGNU.Value;
                        recLogLogradouro.UFE_SG = UFeSg;
                        recLogLogradouro.LOC_NU = CodLocNu.Value;
                        recLogLogradouro.BAI_NU_INI = CodBAINUINI.Value;
                        recLogLogradouro.BAI_NU_FIM = CodBAINUFIM.HasValue ? CodBAINUFIM.Value : CodBAINUFIM;
                        recLogLogradouro.LOG_NO = LOGNO;
                        recLogLogradouro.LOG_COMPLEMENTO = LOGCOMPLEMENTO;
                        recLogLogradouro.CEP = _CEP;
                        recLogLogradouro.TLO_TX = TLOTX;
                        recLogLogradouro.LOG_STA_TLO = LOGSTATLO;
                        recLogLogradouro.LOG_NO_ABREV = LOGNOABREV;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_VAR_LOG
        protected void Button11_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_VAR_LOG.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Var_Log;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String VLOTX = String.Empty;
                    String TLOTX = String.Empty;
                    String VLONU = String.Empty;
                    String LOGNU = String.Empty;

                    Int32 DelimitadorVLOTX = Linha.LastIndexOf('@');
                    if (DelimitadorVLOTX > 0)
                    {
                        VLOTX = Linha.Substring(DelimitadorVLOTX + 1, Linha.Length - DelimitadorVLOTX - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorVLOTX).Trim();
                    }
                    Int32 DelimitadorTLOTX = Linha.LastIndexOf('@');
                    if (DelimitadorTLOTX > 0)
                    {
                        TLOTX = Linha.Substring(DelimitadorTLOTX + 1, Linha.Length - DelimitadorTLOTX - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorTLOTX).Trim();
                    }
                    Int32 DelimitadorVLONU = Linha.LastIndexOf('@');
                    if (DelimitadorVLONU > 0)
                    {
                        VLONU = Linha.Substring(DelimitadorVLONU + 1, Linha.Length - DelimitadorVLONU - 1).Trim();
                        LOGNU = Linha.Substring(0, DelimitadorVLONU).Trim();
                    }

                    Int32? IdLOGNU = null;
                    if (!String.IsNullOrEmpty(LOGNU))
                        IdLOGNU = Convert.ToInt32(LOGNU);

                    Int32? CodVLONU = null;
                    if (!String.IsNullOrEmpty(VLONU))
                        CodVLONU = Convert.ToInt32(VLONU);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.LOG_NU == IdLOGNU.Value && LB.VLO_NU == CodVLONU.Value);
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Var_Log
                        {
                            LOG_NU = IdLOGNU.Value,
                            VLO_NU = CodVLONU.Value,
                            TLO_TX = TLOTX,
                            VLO_TX = VLOTX,
                        };

                        context.Log_Var_Log.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.LOG_NU = IdLOGNU.Value;
                        recLogFaixaUF.VLO_NU = CodVLONU.Value;
                        recLogFaixaUF.TLO_TX = TLOTX;
                        recLogFaixaUF.VLO_TX = VLOTX;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_NUM_SEC
        protected void Button12_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_NUM_SEC.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Num_Sec;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String SECINLADO = String.Empty;
                    String SECNUFIM = String.Empty;
                    String SECNUINI = String.Empty;
                    String LOGNU = String.Empty;

                    Int32 DelimitadorSECINLADO = Linha.LastIndexOf('@');
                    if (DelimitadorSECINLADO > 0)
                    {
                        SECINLADO = Linha.Substring(DelimitadorSECINLADO + 1, Linha.Length - DelimitadorSECINLADO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorSECINLADO).Trim();
                    }
                    Int32 DelimitadorSECNUFIM = Linha.LastIndexOf('@');
                    if (DelimitadorSECNUFIM > 0)
                    {
                        SECNUFIM = Linha.Substring(DelimitadorSECNUFIM + 1, Linha.Length - DelimitadorSECNUFIM - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorSECNUFIM).Trim();
                    }
                    Int32 DelimitadorSECNUINI = Linha.LastIndexOf('@');
                    if (DelimitadorSECNUINI > 0)
                    {
                        SECNUINI = Linha.Substring(DelimitadorSECNUINI + 1, Linha.Length - DelimitadorSECNUINI - 1).Trim();
                        LOGNU = Linha.Substring(0, DelimitadorSECNUINI).Trim();
                    }

                    Int32? IdLOGNU = null;
                    if (!String.IsNullOrEmpty(LOGNU))
                        IdLOGNU = Convert.ToInt32(LOGNU);


                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.LOG_NU == IdLOGNU.Value);
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Num_Sec
                        {
                            LOG_NU = IdLOGNU.Value,
                            SEC_NU_INI = SECNUINI,
                            SEC_NU_FIM = SECNUFIM,
                            SEC_IN_LADO = SECINLADO,
                        };

                        context.Log_Num_Sec.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.LOG_NU = IdLOGNU.Value;
                        recLogFaixaUF.SEC_NU_INI = SECNUINI;
                        recLogFaixaUF.SEC_NU_FIM = SECNUFIM;
                        recLogFaixaUF.SEC_IN_LADO = SECINLADO;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_GRANDE_USUARIO
        protected void Button13_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_GRANDE_USUARIO.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Grande_Usuario;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String GRUNOABREV = String.Empty;
                    String _CEP = String.Empty;
                    String GRUENDERECO = String.Empty;
                    String GRUNO = String.Empty;
                    String LOGNU = String.Empty;
                    String BAINU = String.Empty;
                    String LOCNU = String.Empty;
                    String UFESG = String.Empty;
                    String GRUNU = String.Empty;

                    Int32 DelimitadorGRUNOABREV = Linha.LastIndexOf('@');
                    if (DelimitadorGRUNOABREV > 0)
                    {
                        GRUNOABREV = Linha.Substring(DelimitadorGRUNOABREV + 1, Linha.Length - DelimitadorGRUNOABREV - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorGRUNOABREV).Trim();
                    }

                    Int32 Delimitador_CEP = Linha.LastIndexOf('@');
                    if (Delimitador_CEP > 0)
                    {
                        _CEP = Linha.Substring(Delimitador_CEP + 1, Linha.Length - Delimitador_CEP - 1).Trim();
                        Linha = Linha.Substring(0, Delimitador_CEP).Trim();
                    }

                    Int32 DelimitadorGRUENDERECO = Linha.LastIndexOf('@');
                    if (DelimitadorGRUENDERECO > 0)
                    {
                        GRUENDERECO = Linha.Substring(DelimitadorGRUENDERECO + 1, Linha.Length - DelimitadorGRUENDERECO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorGRUENDERECO).Trim();
                    }

                    Int32 DelimitadorGRUNO = Linha.LastIndexOf('@');
                    if (DelimitadorGRUNO > 0)
                    {
                        GRUNO = Linha.Substring(DelimitadorGRUNO + 1, Linha.Length - DelimitadorGRUNO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorGRUNO).Trim();
                    }

                    Int32 DelimitadorLOGNU = Linha.LastIndexOf('@');
                    if (DelimitadorLOGNU > 0)
                    {
                        LOGNU = Linha.Substring(DelimitadorLOGNU + 1, Linha.Length - DelimitadorLOGNU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOGNU).Trim();
                    }

                    Int32 DelimitadorBAINU = Linha.LastIndexOf('@');
                    if (DelimitadorBAINU > 0)
                    {
                        BAINU = Linha.Substring(DelimitadorBAINU + 1, Linha.Length - DelimitadorBAINU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorBAINU).Trim();
                    }
                    Int32 DelimitadorLOCNU = Linha.LastIndexOf('@');
                    if (DelimitadorLOCNU > 0)
                    {
                        LOCNU = Linha.Substring(DelimitadorLOCNU + 1, Linha.Length - DelimitadorLOCNU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCNU).Trim();
                    }
                    Int32 DelimitadorUFESG = Linha.LastIndexOf('@');
                    if (DelimitadorUFESG > 0)
                    {
                        UFESG = Linha.Substring(DelimitadorUFESG + 1, Linha.Length - DelimitadorUFESG - 1).Trim();
                        GRUNU = Linha.Substring(0, DelimitadorUFESG).Trim();
                    }

                    Int32? IdGRUNU = null;
                    if (!String.IsNullOrEmpty(GRUNU))
                        IdGRUNU = Convert.ToInt32(GRUNU);

                    Int32? CodLOCNU = null;
                    if (!String.IsNullOrEmpty(LOCNU))
                        CodLOCNU = Convert.ToInt32(LOCNU);

                    Int32? CodBAINU = null;
                    if (!String.IsNullOrEmpty(BAINU))
                        CodBAINU = Convert.ToInt32(BAINU);

                    Int32? CodLOGNU = null;
                    if (!String.IsNullOrEmpty(LOGNU))
                        CodLOGNU = Convert.ToInt32(LOGNU);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.GRU_NU == IdGRUNU.Value);
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Grande_Usuario
                        {
                            GRU_NU = IdGRUNU.Value,
                            UFE_SG = UFESG,
                            LOC_NU = CodLOCNU.Value,
                            BAI_NU = CodBAINU.Value,
                            LOG_NU = CodLOGNU,
                            GRU_NO = GRUNO,
                            GRU_ENDERECO = GRUENDERECO,
                            CEP = _CEP,
                            GRU_NO_ABREV = GRUNOABREV,
                        };

                        context.Log_Grande_Usuario.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.GRU_NU = IdGRUNU.Value;
                        recLogFaixaUF.UFE_SG = UFESG;
                        recLogFaixaUF.LOC_NU = CodLOCNU.Value;
                        recLogFaixaUF.BAI_NU = CodBAINU.Value;
                        recLogFaixaUF.LOG_NU = CodLOGNU;
                        recLogFaixaUF.GRU_NO = GRUNO;
                        recLogFaixaUF.GRU_ENDERECO = GRUENDERECO;
                        recLogFaixaUF.CEP = _CEP;
                        recLogFaixaUF.GRU_NO_ABREV = GRUNOABREV;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_UNID_OPER
        protected void Button14_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_UNID_OPER.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Unid_Oper;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String UOPNOABREV = String.Empty;
                    String UOPINCP = String.Empty;
                    String _CEP = String.Empty;
                    String UOPENDERECO = String.Empty;
                    String UOPNO = String.Empty;
                    String LOGNU = String.Empty;
                    String BAINU = String.Empty;
                    String LOCNU = String.Empty;
                    String UFESG = String.Empty;
                    String UOPNU = String.Empty;

                    Int32 DelimitadorUOPNOABREV = Linha.LastIndexOf('@');
                    if (DelimitadorUOPNOABREV > 0)
                    {
                        UOPNOABREV = Linha.Substring(DelimitadorUOPNOABREV + 1, Linha.Length - DelimitadorUOPNOABREV - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUOPNOABREV).Trim();
                    }

                    Int32 DelimitadorUOPINCP = Linha.LastIndexOf('@');
                    if (DelimitadorUOPINCP > 0)
                    {
                        UOPINCP = Linha.Substring(DelimitadorUOPINCP + 1, Linha.Length - DelimitadorUOPINCP - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUOPINCP).Trim();
                    }

                    Int32 Delimitador_CEP = Linha.LastIndexOf('@');
                    if (Delimitador_CEP > 0)
                    {
                        _CEP = Linha.Substring(Delimitador_CEP + 1, Linha.Length - Delimitador_CEP - 1).Trim();
                        Linha = Linha.Substring(0, Delimitador_CEP).Trim();
                    }

                    Int32 DelimitadorUOPENDERECO = Linha.LastIndexOf('@');
                    if (DelimitadorUOPENDERECO > 0)
                    {
                        UOPENDERECO = Linha.Substring(DelimitadorUOPENDERECO + 1, Linha.Length - DelimitadorUOPENDERECO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUOPENDERECO).Trim();
                    }

                    Int32 DelimitadorUOPNO = Linha.LastIndexOf('@');
                    if (DelimitadorUOPNO > 0)
                    {
                        UOPNO = Linha.Substring(DelimitadorUOPNO + 1, Linha.Length - DelimitadorUOPNO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUOPNO).Trim();
                    }

                    Int32 DelimitadorLOGNU = Linha.LastIndexOf('@');
                    if (DelimitadorLOGNU > 0)
                    {
                        LOGNU = Linha.Substring(DelimitadorLOGNU + 1, Linha.Length - DelimitadorLOGNU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOGNU).Trim();
                    }

                    Int32 DelimitadorBAINU = Linha.LastIndexOf('@');
                    if (DelimitadorBAINU > 0)
                    {
                        BAINU = Linha.Substring(DelimitadorBAINU + 1, Linha.Length - DelimitadorBAINU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorBAINU).Trim();
                    }
                    Int32 DelimitadorLOCNU = Linha.LastIndexOf('@');
                    if (DelimitadorLOCNU > 0)
                    {
                        LOCNU = Linha.Substring(DelimitadorLOCNU + 1, Linha.Length - DelimitadorLOCNU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCNU).Trim();
                    }
                    Int32 DelimitadorUFESG = Linha.LastIndexOf('@');
                    if (DelimitadorUFESG > 0)
                    {
                        UFESG = Linha.Substring(DelimitadorUFESG + 1, Linha.Length - DelimitadorUFESG - 1).Trim();
                        UOPNU = Linha.Substring(0, DelimitadorUFESG).Trim();
                    }

                    Int32? IdUOPNU = null;
                    if (!String.IsNullOrEmpty(UOPNU))
                        IdUOPNU = Convert.ToInt32(UOPNU);

                    Int32? CodLOCNU = null;
                    if (!String.IsNullOrEmpty(LOCNU))
                        CodLOCNU = Convert.ToInt32(LOCNU);

                    Int32? CodBAINU = null;
                    if (!String.IsNullOrEmpty(BAINU))
                        CodBAINU = Convert.ToInt32(BAINU);

                    Int32? CodLOGNU = null;
                    if (!String.IsNullOrEmpty(LOGNU))
                        CodLOGNU = Convert.ToInt32(LOGNU);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.UOP_NU == IdUOPNU.Value);
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Log_Unid_Oper
                        {
                            UOP_NU = IdUOPNU.Value,
                            UFE_SG = UFESG,
                            LOC_NU = CodLOCNU.Value,
                            BAI_NU = CodBAINU.Value,
                            LOG_NU = CodLOGNU,
                            UOP_NO = UOPNO,
                            UOP_ENDERECO = UOPENDERECO,
                            CEP = _CEP,
                            UOP_IN_CP = UOPINCP,
                            UOP_NO_ABREV = UOPNOABREV,
                        };

                        context.Log_Unid_Oper.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.UOP_NU = IdUOPNU.Value;
                        recLogFaixaUF.UFE_SG = UFESG;
                        recLogFaixaUF.LOC_NU = CodLOCNU.Value;
                        recLogFaixaUF.BAI_NU = CodBAINU.Value;
                        recLogFaixaUF.LOG_NU = CodLOGNU;
                        recLogFaixaUF.UOP_NO = UOPNO;
                        recLogFaixaUF.UOP_ENDERECO = UOPENDERECO;
                        recLogFaixaUF.CEP = _CEP;
                        recLogFaixaUF.UOP_IN_CP = UOPINCP;
                        recLogFaixaUF.UOP_NO_ABREV = UOPNOABREV;
                        context.SaveChanges();
                    }
                }
            }
        }

        // LOG_FAIXA_UOP
        protected void Button15_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\LOG_FAIXA_UOP.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Faixa_Uop;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String FNCFINAL = String.Empty;
                    String FNCINICIAL = String.Empty;
                    String UOPNU = String.Empty;

                    Int32 DelimitadorFCBCEPFIM = Linha.LastIndexOf('@');
                    if (DelimitadorFCBCEPFIM > 0)
                    {
                        FNCFINAL = Linha.Substring(DelimitadorFCBCEPFIM + 1, Linha.Length - DelimitadorFCBCEPFIM - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorFCBCEPFIM).Trim();
                    }
                    Int32 DelimitadorUOPNU = Linha.LastIndexOf('@');
                    if (DelimitadorUOPNU > 0)
                    {
                        FNCINICIAL = Linha.Substring(DelimitadorUOPNU + 1, Linha.Length - DelimitadorUOPNU - 1).Trim();
                        UOPNU = Linha.Substring(0, DelimitadorUOPNU).Trim();
                    }

                    Int32? IdUOPNU = null;
                    if (!String.IsNullOrEmpty(UOPNU))
                        IdUOPNU = Convert.ToInt32(UOPNU);

                    Int32? CodFNCINICIAL = null;
                    if (!String.IsNullOrEmpty(UOPNU))
                        CodFNCINICIAL = Convert.ToInt32(UOPNU);

                    Int32? CodFNCFINAL = null;
                    if (!String.IsNullOrEmpty(FNCFINAL))
                        CodFNCFINAL = Convert.ToInt32(FNCFINAL);

                    var recLogFaixaBairro = recLog.FirstOrDefault(LB => LB.UOP_NU == IdUOPNU.Value && LB.FNC_INICIAL == CodFNCINICIAL);
                    if (recLogFaixaBairro == null)
                    {
                        var LogFaixaBairro = new Log_Faixa_Uop
                        {
                            UOP_NU = IdUOPNU.Value,
                            FNC_INICIAL = CodFNCINICIAL.Value,
                            FNC_FINAL = CodFNCFINAL.Value,
                        };

                        context.Log_Faixa_Uop.Add(LogFaixaBairro);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaBairro.UOP_NU = IdUOPNU.Value;
                        recLogFaixaBairro.FNC_INICIAL = CodFNCINICIAL.Value;
                        recLogFaixaBairro.FNC_FINAL = CodFNCFINAL.Value;
                        context.SaveChanges();
                    }
                }
            }
        }

        // ECT_PAIS
        protected void Button16_Click(object sender, EventArgs e)
        {
            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Delimitado\ECT_PAIS.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Ect_Pais;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String PAIABREVIATURA = String.Empty;
                    String PAINOFRANCES = String.Empty;
                    String PAINOINGLES = String.Empty;
                    String PAINOPORTUGUES = String.Empty;
                    String PAISGALTERNATIVA = String.Empty;
                    String PAISG = String.Empty;

                    Int32 DelimitadorUOPNO = Linha.LastIndexOf('@');
                    if (DelimitadorUOPNO > 0)
                    {
                        PAIABREVIATURA = Linha.Substring(DelimitadorUOPNO + 1, Linha.Length - DelimitadorUOPNO - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUOPNO).Trim();
                    }

                    Int32 DelimitadorLOGNU = Linha.LastIndexOf('@');
                    if (DelimitadorLOGNU > 0)
                    {
                        PAINOFRANCES = Linha.Substring(DelimitadorLOGNU + 1, Linha.Length - DelimitadorLOGNU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOGNU).Trim();
                    }

                    Int32 DelimitadorBAINU = Linha.LastIndexOf('@');
                    if (DelimitadorBAINU > 0)
                    {
                        PAINOINGLES = Linha.Substring(DelimitadorBAINU + 1, Linha.Length - DelimitadorBAINU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorBAINU).Trim();
                    }
                    Int32 DelimitadorLOCNU = Linha.LastIndexOf('@');
                    if (DelimitadorLOCNU > 0)
                    {
                        PAINOPORTUGUES = Linha.Substring(DelimitadorLOCNU + 1, Linha.Length - DelimitadorLOCNU - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorLOCNU).Trim();
                    }
                    Int32 DelimitadorUFESG = Linha.LastIndexOf('@');
                    if (DelimitadorUFESG > 0)
                    {
                        PAISGALTERNATIVA = Linha.Substring(DelimitadorUFESG + 1, Linha.Length - DelimitadorUFESG - 1).Trim();
                        PAISG = Linha.Substring(0, DelimitadorUFESG).Trim();
                    }

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.PAI_SG.Equals(PAISG));
                    if (recLogFaixaUF == null)
                    {
                        var LogFaixaUF = new Ect_Pais
                        {
                            PAI_SG = PAISG,
                            PAI_SG_ALTERNATIVA = PAISGALTERNATIVA,
                            PAI_NO_PORTUGUES = PAINOPORTUGUES,
                            PAI_NO_INGLES = PAINOINGLES,
                            PAI_NO_FRANCES = PAINOFRANCES,
                            PAI_ABREVIATURA = PAIABREVIATURA,
                        };

                        context.Ect_Pais.Add(LogFaixaUF);
                        context.SaveChanges();
                    }
                    else
                    {
                        recLogFaixaUF.PAI_SG = PAISG;
                        recLogFaixaUF.PAI_SG_ALTERNATIVA = PAISGALTERNATIVA;
                        recLogFaixaUF.PAI_NO_PORTUGUES = PAINOPORTUGUES;
                        recLogFaixaUF.PAI_NO_INGLES = PAINOINGLES;
                        recLogFaixaUF.PAI_NO_FRANCES = PAINOFRANCES;
                        recLogFaixaUF.PAI_ABREVIATURA = PAIABREVIATURA;
                        context.SaveChanges();
                    }
                }
            }

        }

        protected void Button17_Click(object sender, EventArgs e)
        {

            // Observacao ...... Salvar aquivo com  Encoding  UTF8
            string[] lines = System.IO.File.ReadAllLines(@"C:\Fontes\ImportadoDeCPs\eDNE_Basico_1503\Cad_Municipio_Radar.txt", System.Text.Encoding.UTF8);
            using (var context = new dneEntities())
            {
                var recLog = context.Log_Localidade;
                foreach (string line in lines)
                {
                    String Linha = line;
                    String Ultimo = String.Empty;
                    String LOCCEPFIM = String.Empty;
                    String LOCCEPINI = String.Empty;
                    String LOCNU = String.Empty;

                    Int32 DelimitadorUltimo = Linha.LastIndexOf('|');
                    if (DelimitadorUltimo > 0)
                    {
                        Ultimo = Linha.Substring(DelimitadorUltimo + 1, Linha.Length - DelimitadorUltimo - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUltimo).Trim();
                    }

                    Int32 DelimitadorUFECEPFIM = Linha.LastIndexOf('|');
                    if (DelimitadorUFECEPFIM > 0)
                    {
                        LOCCEPFIM = Linha.Substring(DelimitadorUFECEPFIM + 1, Linha.Length - DelimitadorUFECEPFIM - 1).Trim();
                        Linha = Linha.Substring(0, DelimitadorUFECEPFIM).Trim();
                    }
                    Int32 DelimitadorUFECEPINI = Linha.LastIndexOf('|');
                    if (DelimitadorUFECEPINI > 0)
                    {
                        LOCCEPINI = Linha.Substring(DelimitadorUFECEPINI + 1, Linha.Length - DelimitadorUFECEPINI - 1).Trim();
                        LOCNU = Linha.Substring(0, DelimitadorUFECEPINI).Trim();
                    }

                    Int32? IdLOCNU = null;
                    if (!String.IsNullOrEmpty(LOCNU))
                        IdLOCNU = Convert.ToInt32(LOCNU);

                    var recLogFaixaUF = recLog.FirstOrDefault(LB => LB.MUN_NU.Equals(Ultimo));
                    if (recLogFaixaUF != null)
                    {
                        //string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        //using (StreamWriter outfile = new StreamWriter(mydocpath + @"\UserInputFile.txt", true))
                        //{
                        //    outfile.WriteLine(LOCNU);
                        //}
                        recLogFaixaUF.CAD_MUNI_RADAR = IdLOCNU.Value;
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}

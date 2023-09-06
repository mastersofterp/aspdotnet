using System;
public class StudentFees
{
    private int _userno = 0;
    private int _recon = 0;
    private int _feemode = 0;
    private int _provstatus = 0;
    private string _sessionno = string.Empty;
    private double _amount = 0;
    private string _feeType = string.Empty;
    private string _ddcheqno = string.Empty;
    private int _bankno = 0;
    private string _bankname = string.Empty;
    private string _branchname = string.Empty;
    private int _ddno = 0;
    private string _sendername = string.Empty;
    private string _transid = string.Empty;
    private string _ReceiptNO = string.Empty;
    private int _unitno = 0;
    private Boolean _catapplied;
    private DateTime _transdate = System.DateTime.Today;
 private string _OrderID = string.Empty;
    public int UserNo
    {
        get { return _userno; }
        set { _userno = value; }
    }
    public string SessionNo
    {
        get { return _sessionno; }
        set { _sessionno = value; }
    }
    public int Bankno
    {
        get { return _bankno; }
        set { _bankno = value; }
    }
    public double Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
    public int Ddno
    {
        get { return _ddno; }
        set { _ddno = value; }
    }
    public string FeeType
    {
        get { return _feeType; }
        set { _feeType = value; }
    }
    public string DDChequeno
    {
        get { return _ddcheqno; }
        set { _ddcheqno = value; }
    }
    public string BankName
    {
        get { return _bankname; }
        set { _bankname = value; }
    }
    public string BranchName
    {
        get { return _branchname; }
        set { _branchname = value; }
    }
    public int Recon
    {
        get { return _recon; }
        set { _recon = value; }
    }
    public int Feemode
    {
        get { return _feemode; }
        set { _feemode = value; }
    }
    public int provstatus
    {
        get { return _provstatus; }
        set { _provstatus = value; }

    }
    public string SenderName
    {
        get { return _sendername; }
        set { _sendername = value; }
    }
    public string TransID
    {
        get { return _transid; }
        set { _transid = value; }
    }
    public DateTime TransDate
    {
        get { return _transdate; }
        set { _transdate = value; }
    }
    public int Unitno
    {
        get { return _unitno; }
        set { _unitno = value; }
    }
    public Boolean Catapplied
    {
        get { return _catapplied; }
        set { _catapplied = value; }
    }
    public string ReceiptNo
    {
        get
        {
            return _ReceiptNO;
        }
        set
        {
            _ReceiptNO = value;
        }
    }
    public string OrderID
    {
        get { return _OrderID; }
        set { _OrderID = value; }
    }
}

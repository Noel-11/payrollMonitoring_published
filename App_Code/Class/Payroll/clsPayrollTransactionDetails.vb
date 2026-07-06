Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollTransactionDetails

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property transactionDetailsId As String

    Public Property transactionHeaderId As String

    Public Property transactionEmployeeId As String

    Public Property employeeId As String

    Public Property payrollItemType As String

    Public Property cdId As String

    Public Property cdType As String

    Public Property cdName As String

    Public Property cdAmount As String

    Public Property cdAddType As String

    Public Property cdAgency As String

    Public Property cdColumnNumber As String

    Public Property sortOrder As String

    Public Property remittanceId As String

    Public Property isActive As String

    Public Property createDatetime As String

    Public Property prevTransactionHeaderId As String

#End Region

    Public Sub initialize()

        _transactionDetailsId = ""
        _transactionHeaderId = ""
        _prevTransactionHeaderId = ""
        _transactionEmployeeId = ""
        _employeeId = ""
        _payrollItemType = ""
        _cdId = ""
        _cdType = ""
        _cdName = ""
        _cdAmount = "0.0"
        _cdAddType = ""
        _cdColumnNumber = "0"
        _sortOrder = "0"
        _isActive = "Y"
        _createDatetime = ""
        _cdAgency = ""
        _remittanceId = ""


    End Sub

    Public Function browsePayrollTransactionDetailsForLog(ByVal _thisID As String) As DataTable
        Dim sql As String = ""

        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, cd_amount, cd_add_type, sort_order, create_datetime, FROM tbl_payroll_transaction_details " & _
              "WHERE transaction_details_id='" & _thisID & "' LIMIT 1"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function

    Public Function browsePayrollTransactionDetails(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, ByVal thisCD As String) As DataTable

        Dim sql As String = ""

        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, cd_amount, cd_add_type, tbl_payroll_transaction_details.sort_order, " & _
              "(CASE WHEN deduction_role = 'OPTIONAL' THEN 'TRUE' ELSE 'FALSE' END) AS isOptional,remittance_id FROM tbl_payroll_transaction_details " & _
              "LEFT JOIN tbl_payroll_ref_deduction ON tbl_payroll_transaction_details.cd_id = tbl_payroll_ref_deduction.deduction_id " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND " & _
              "payroll_item_type='" & thisCD & "' AND cd_amount > 0 AND tbl_payroll_transaction_details.is_active = 'Y' ORDER BY tbl_payroll_transaction_details.sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function


    Public Function browsePayrollTransactionDetails2(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, ByVal thisCD As String) As DataTable

        Dim sql As String = ""

        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, SUM(cd_amount) AS cd_amount, cd_add_type, tbl_payroll_transaction_details.sort_order, " & _
              "(CASE WHEN deduction_role = 'OPTIONAL' THEN 'TRUE' ELSE 'FALSE' END) AS isOptional FROM tbl_payroll_transaction_details " & _
              "LEFT JOIN tbl_payroll_ref_deduction ON tbl_payroll_transaction_details.cd_id = tbl_payroll_ref_deduction.deduction_id " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND " & _
              "payroll_item_type='" & thisCD & "' AND cd_amount > 0 AND tbl_payroll_transaction_details.is_active = 'Y' " & _
              "GROUP BY cd_id " & _
              "ORDER BY tbl_payroll_transaction_details.sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function

    Public Function browsePayrollTransactionDetailsAgency(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, ByVal thisCD As String, ByVal thisAgency As String) As DataTable

        Dim sql As String = ""

        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, cd_amount, cd_add_type, tbl_payroll_transaction_details.sort_order, " & _
              "(CASE WHEN deduction_role = 'OPTIONAL' THEN 'TRUE' ELSE 'FALSE' END) AS isOptional FROM tbl_payroll_transaction_details " & _
              "LEFT JOIN tbl_payroll_ref_deduction ON tbl_payroll_transaction_details.cd_id = tbl_payroll_ref_deduction.deduction_id " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND " & _
              "payroll_item_type='" & thisCD & "' AND cd_amount > 0 AND tbl_payroll_transaction_details.is_active = 'Y' AND tbl_payroll_transaction_details.cd_agency = '" & thisAgency & "' ORDER BY tbl_payroll_transaction_details.sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function

    Public Function browsePayrollTransactionDetailsGSPS(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, Optional thisCD As String = "") As DataTable
        Dim sql As String = ""

        Dim _where As String = ""

        If thisCD <> "" Then
            _where += " AND payroll_item_type='" & thisCD & "'"
        Else
            _where += " AND payroll_item_type IN ('DEDUCTION','GOVERNMENT')"
        End If

        'sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, cd_amount, cd_add_type, tbl_payroll_transaction_details.sort_order, " & _
        '      "(CASE WHEN deduction_role = 'OPTIONAL' THEN 'TRUE' ELSE 'FALSE' END) AS isOptional FROM tbl_payroll_transaction_details " & _
        '      "LEFT JOIN tbl_payroll_ref_deduction ON tbl_payroll_transaction_details.cd_id = tbl_payroll_ref_deduction.deduction_id " & _
        '      "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND cd_id <> 'ECC' AND cd_amount>0 " & _where & " ORDER BY tbl_payroll_transaction_details.sort_order"

        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, cd_amount, cd_add_type, tbl_payroll_transaction_details.sort_order, " & _
           "(CASE WHEN deduction_role = 'OPTIONAL' THEN 'TRUE' ELSE 'FALSE' END) AS isOptional FROM tbl_payroll_transaction_details " & _
           "LEFT JOIN tbl_payroll_ref_deduction ON tbl_payroll_transaction_details.cd_id = tbl_payroll_ref_deduction.deduction_id " & _
           "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND cd_amount>0 " & _where & " ORDER BY tbl_payroll_transaction_details.sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function

    Public Function browsePayrollTransactionDetailsRemittance(ByVal thisEmployeeID As String, ByVal thisAgency As String, _thisRemittance As String, ByVal _thisPeriod As String) As DataTable

        Dim sql As String = ""

        'sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, " & _
        '      "cd_name, SUM(cd_amount) AS cd_amount, cd_add_type, sort_order FROM tbl_payroll_transaction_details " & _
        '      "WHERE transaction_employee_id = '" & thisEmployeeID & "' AND cd_agency='" & thisAgency & "' AND remittance_id = '" & _thisRemittance & "' AND cd_amount>0 " & _
        '      "GROUP BY tbl_payroll_transaction_details.employee_id,cd_id ORDER BY sort_order"

        'sql = "SELECT transaction_details_id, tbl_payroll_transaction_employees.transaction_header_id, transaction_employee_id, tbl_payroll_transaction_employees.employee_id, payroll_item_type, " & _
        '      "tbl_payroll_transaction_details.cd_id, cd_type, tbl_payroll_transaction_details.cd_name, SUM(tbl_payroll_transaction_details.cd_amount) AS cd_amount, cd_add_type, tbl_payroll_transaction_details.sort_order, " & _
        '      "CONCAT(UPPER(MONTHNAME(STR_TO_DATE(emp_payroll_month,'%m'))),emp_payroll_year) AS perCov FROM tbl_payroll_transaction_details " & _
        '      "INNER JOIN tbl_payroll_transaction_employees ON tbl_payroll_transaction_details.transaction_employee_id = tbl_payroll_transaction_employees.transaction_employees_id " & _
        '      "INNER JOIN tbl_payroll_deduction_remittance_details ON tbl_payroll_transaction_details.remittance_id = tbl_payroll_deduction_remittance_details.deduction_remittance_details_id " & _
        '      "INNER JOIN tbl_payroll_ref_deduction ON tbl_payroll_transaction_details.cd_id = tbl_payroll_ref_deduction.deduction_id " & _
        '      "WHERE tbl_payroll_transaction_employees.employee_id = '" & thisEmployeeID & "' AND tbl_payroll_ref_deduction.agency_id ='" & thisAgency & "' AND tbl_payroll_transaction_details.remittance_id = '" & _thisRemittance & "' AND tbl_payroll_transaction_details.cd_amount>0 " & _
        '      "GROUP BY tbl_payroll_transaction_details.employee_id,cd_id,perCov " & _
        '      "ORDER BY sort_order "

        sql = "SELECT transaction_details_id, tbl_payroll_transaction_employees.transaction_header_id, transaction_employee_id, tbl_payroll_transaction_employees.employee_id, payroll_item_type, " & _
            "tbl_payroll_transaction_details.cd_id, cd_type, tbl_payroll_transaction_details.cd_name, SUM(tbl_payroll_transaction_details.cd_amount) AS cd_amount, cd_add_type, tbl_payroll_transaction_details.sort_order, " & _
            "CONCAT(UPPER(MONTHNAME(STR_TO_DATE(emp_payroll_month,'%m'))),emp_payroll_year) AS perCov FROM tbl_payroll_transaction_details " & _
            "INNER JOIN tbl_payroll_transaction_employees ON tbl_payroll_transaction_details.transaction_employee_id = tbl_payroll_transaction_employees.transaction_employees_id " & _
            "INNER JOIN tbl_payroll_deduction_remittance_details ON tbl_payroll_transaction_details.remittance_id = tbl_payroll_deduction_remittance_details.deduction_remittance_details_id " & _
            "INNER JOIN tbl_payroll_ref_deduction ON tbl_payroll_transaction_details.cd_id = tbl_payroll_ref_deduction.deduction_id " & _
            "WHERE tbl_payroll_transaction_employees.employee_id = '" & thisEmployeeID & "' AND tbl_payroll_ref_deduction.agency_id ='" & thisAgency & "' AND tbl_payroll_deduction_remittance_details.deduction_remittance_header_id = '" & _thisRemittance & "' AND " & _
            "CONCAT(UPPER(MONTHNAME(STR_TO_DATE(emp_payroll_month,'%m'))),emp_payroll_year) = '" & _thisPeriod & "' AND tbl_payroll_transaction_details.cd_amount>0 " & _
            "GROUP BY cd_id,tbl_payroll_deduction_remittance_details.deduction_remittance_details_id " & _
            "ORDER BY sort_order "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function

    'Public Function browsePayrollFixTransactionDetails(ByVal thisHeaderID As String, ByVal thisEmployeeID As String) As DataTable
    '    Dim sql As String = ""

    '    sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, " & _
    '          "(CASE WHEN cd_id <> 'ECC' THEN cd_amount ELSE 0.00 END) AS cd_amount, cd_add_type, tblPayrollDetailsMain.sort_order, " & _
    '          "COALESCE((SELECT cd_amount FROM tbl_payroll_transaction_details WHERE tbl_payroll_transaction_details.transaction_employee_id = tblPayrollDetailsMain.transaction_employee_id AND " & _
    '          "tbl_payroll_transaction_details.cd_agency = tblPayrollDetailsMain.cd_agency AND tbl_payroll_transaction_details.payroll_item_type = 'GOVERNMENT' AND " & _
    '          "tbl_payroll_transaction_details.cd_type IN ('SHARE','GOVERNMENT')  LIMIT 1),0.0) AS govShare FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
    '          "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND (payroll_item_type='FIX DEDUCTION' OR cd_id = 'ECC') AND cd_amount>0 AND cd_id NOT IN ('WITHHOLDING TAX','WITHHOLDING TAX 1%') ORDER BY sort_order"

    '    Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")
    'End Function

     Public Function browsePayrollFixTransactionDetails(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, Optional _thisType As String = "") As DataTable
        Dim sql As String = ""

        Dim _sqlWhere As String = ""

        If _thisType = "TAX" Then
            _sqlWhere += "AND (deduction_type = '" & _thisType & "')"

            sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, " & _
      "(CASE WHEN cd_id <> 'ECC' THEN cd_amount ELSE 0.00 END) AS cd_amount, cd_add_type, tblPayrollDetailsMain.sort_order, " & _
      "0.0 AS govShare, " & _
      "'' AS govShareID " & _
      "FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
      "LEFT JOIN tbl_payroll_ref_fix_deduction ON tblPayrollDetailsMain.cd_id = tbl_payroll_ref_fix_deduction.deduction_code " & _
      "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND (payroll_item_type='FIX DEDUCTION') AND cd_amount>0 " & _sqlWhere & _
      "ORDER BY tblPayrollDetailsMain.sort_order"

        Else
            If _thisType <> "" Then
                _sqlWhere += "AND (cd_type = '" & _thisType & "' OR COALESCE(cd_type,'') = '') "
            End If




            sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, " & _
               "(CASE WHEN cd_id <> 'ECC' THEN cd_agency ELSE cd_name END) AS cd_name, " & _
               "cd_add_type, tblPayrollDetailsMain.sort_order,agency_id, " & _
               "SUM(CASE WHEN payroll_item_type='FIX DEDUCTION' THEN cd_amount ELSE 0 END) AS cd_amount, " & _
               "SUM(CASE WHEN payroll_item_type='GOVERNMENT' AND cd_type = 'SHARE' THEN cd_amount ELSE 0 END) AS govShare, " & _
               "GROUP_CONCAT(CASE WHEN payroll_item_type='GOVERNMENT' AND cd_type = 'SHARE' THEN transaction_details_id ELSE NULL END) AS govShareID, " & _
               "GROUP_CONCAT(CASE WHEN payroll_item_type<>'GOVERNMENT' AND cd_type = 'SHARE' THEN transaction_details_id ELSE NULL END) AS perShareID " & _
               "FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
               "INNER JOIN tbl_payroll_ref_deduction ON tblPayrollDetailsMain.cd_id = tbl_payroll_ref_deduction.deduction_id " & _
               "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND " & _
               "(payroll_item_type='FIX DEDUCTION' OR payroll_item_type='GOVERNMENT') AND cd_id <> 'ECC' AND cd_amount>0 " & _sqlWhere & _
               "GROUP BY tbl_payroll_ref_deduction.agency_id " & _
               "UNION ALL " & _
               "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, " & _
               "cd_add_type, tblPayrollDetailsMain.sort_order,cd_agency AS agency_id, " & _
               "0.0 AS cd_amount, cd_amount AS govShare,transaction_details_id AS govShareID, '' AS perShareID " & _
               "FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
               "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND cd_id = 'ECC' " & _
               "ORDER BY sort_order"


        End If

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function


    Public Function browsePayrollFixTransactionDetailsJO(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, Optional _thisType As String = "") As DataTable
        Dim sql As String = ""

        Dim _sqlWhere As String = ""

        If _thisType <> "" Then
            If _thisType = "TAX" Then
                _sqlWhere += "AND (deduction_type = '" & _thisType & "')"
            Else
                _sqlWhere += "AND (deduction_type = '" & _thisType & "' OR COALESCE(deduction_type,'') = '') "
            End If

        End If


        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, " & _
           "(CASE WHEN cd_id <> 'ECC' THEN cd_amount ELSE 0.00 END) AS cd_amount, cd_add_type, tblPayrollDetailsMain.sort_order, " & _
           "0.00 AS govShare, " & _
           "'' AS govShareID " & _
           "FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
           "LEFT JOIN tbl_payroll_ref_fix_deduction ON tblPayrollDetailsMain.cd_id = tbl_payroll_ref_fix_deduction.deduction_id " & _
           "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND (payroll_item_type='FIX DEDUCTION' OR cd_id = 'ECC') AND cd_amount>0 " & _sqlWhere & _
           "ORDER BY tblPayrollDetailsMain.sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")
    End Function

    Public Function browsePayrollFixTransactionDetails2(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, Optional _thisType As String = "") As DataTable
        Dim sql As String = ""

        Dim _sqlWhere As String = ""

        If _thisType <> "" Then
            If _thisType = "TAX" Then
                _sqlWhere += "AND (deduction_type = '" & _thisType & "')"
            Else
                _sqlWhere += "AND (deduction_type = '" & _thisType & "' OR COALESCE(deduction_type,'') = '') "
            End If

        End If



        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, " & _
              "(CASE WHEN cd_id <> 'ECC' THEN SUM(cd_amount) ELSE 0.00 END) AS cd_amount, cd_add_type, tblPayrollDetailsMain.sort_order, COALESCE(( " & _
              "Select SUM(cd_amount) " & _
              "FROM tbl_payroll_transaction_details " & _
              "WHERE tbl_payroll_transaction_details.transaction_employee_id = tblPayrollDetailsMain.transaction_employee_id AND tbl_payroll_transaction_details.cd_agency = tblPayrollDetailsMain.cd_agency AND " & _
              "tbl_payroll_transaction_details.payroll_item_type = 'GOVERNMENT' AND tbl_payroll_transaction_details.cd_type = tblPayrollDetailsMain.cd_type " & _
              "LIMIT 1),0.0) AS govShare, COALESCE(( " & _
              "SELECT transaction_details_id " & _
              "FROM tbl_payroll_transaction_details " & _
              "WHERE tbl_payroll_transaction_details.transaction_employee_id = tblPayrollDetailsMain.transaction_employee_id AND tbl_payroll_transaction_details.cd_agency = tblPayrollDetailsMain.cd_agency AND " & _
              "tbl_payroll_transaction_details.payroll_item_type = 'GOVERNMENT' AND tbl_payroll_transaction_details.cd_type = tblPayrollDetailsMain.cd_type " & _
              "LIMIT 1),'') AS govShareID " & _
              "FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
              "LEFT JOIN tbl_payroll_ref_fix_deduction ON tblPayrollDetailsMain.cd_id = tbl_payroll_ref_fix_deduction.deduction_id " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND employee_id = '" & thisEmployeeID & "' AND (payroll_item_type='FIX DEDUCTION' OR cd_id = 'ECC') AND cd_amount>0 " & _sqlWhere & _
              "GROUP BY cd_id " & _
              "ORDER BY tblPayrollDetailsMain.sort_order"



        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function

    Public Function browsePayrollFixTransactionDetailsAgency(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, ByVal thisAgency As String, Optional _thisType As String = "") As DataTable
        Dim sql As String = ""

        Dim _sqlWhere As String = ""

        If _thisType <> "" Then
            If _thisType = "TAX" Then
                _sqlWhere += "AND (deduction_type = '" & _thisType & "')"
            Else
                _sqlWhere += "AND (deduction_type = '" & _thisType & "' OR COALESCE(deduction_type,'') = '') "
            End If

        End If

        'sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, " & _
        '      "(CASE WHEN cd_id <> 'ECC' THEN cd_amount ELSE 0.00 END) AS cd_amount, cd_add_type, tblPayrollDetailsMain.sort_order, " & _
        '      "COALESCE((SELECT cd_amount FROM tbl_payroll_transaction_details WHERE tbl_payroll_transaction_details.transaction_employee_id = tblPayrollDetailsMain.transaction_employee_id AND " & _
        '      "tbl_payroll_transaction_details.cd_agency = tblPayrollDetailsMain.cd_agency AND tbl_payroll_transaction_details.payroll_item_type = 'GOVERNMENT' AND " & _
        '      "tbl_payroll_transaction_details.cd_type IN ('SHARE','GOVERNMENT')  LIMIT 1),0.0) AS govShare FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
        '      "LEFT JOIN tbl_payroll_ref_fix_deduction ON tblPayrollDetailsMain.cd_id = tbl_payroll_ref_fix_deduction.deduction_id " & _
        '      "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND (payroll_item_type='FIX DEDUCTION' OR cd_id = 'ECC') AND cd_amount>0 " & _sqlWhere & _
        '      "ORDER BY tblPayrollDetailsMain.sort_order"

        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, " & _
              "(CASE WHEN cd_id <> 'ECC' THEN cd_amount ELSE 0.00 END) AS cd_amount, cd_add_type, tblPayrollDetailsMain.sort_order, " & _
              "COALESCE((SELECT cd_amount FROM tbl_payroll_transaction_details WHERE tbl_payroll_transaction_details.transaction_employee_id = tblPayrollDetailsMain.transaction_employee_id AND " & _
              "tbl_payroll_transaction_details.cd_agency = tblPayrollDetailsMain.cd_agency AND tbl_payroll_transaction_details.payroll_item_type = 'GOVERNMENT' AND " & _
              "tbl_payroll_transaction_details.cd_type = tblPayrollDetailsMain.cd_type LIMIT 1),0.0) AS govShare, " & _
              "COALESCE((Select transaction_details_id FROM tbl_payroll_transaction_details WHERE tbl_payroll_transaction_details.transaction_employee_id = tblPayrollDetailsMain.transaction_employee_id AND " & _
              "tbl_payroll_transaction_details.cd_agency = tblPayrollDetailsMain.cd_agency AND tbl_payroll_transaction_details.payroll_item_type = 'GOVERNMENT' AND tbl_payroll_transaction_details.cd_type = tblPayrollDetailsMain.cd_type " & _
              "LIMIT 1),'') AS govShareID " & _
              "FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
              "LEFT JOIN tbl_payroll_ref_fix_deduction ON tblPayrollDetailsMain.cd_id = tbl_payroll_ref_fix_deduction.deduction_id " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND (payroll_item_type='FIX DEDUCTION' OR cd_id = 'ECC') AND cd_amount>0 AND tblPayrollDetailsMain.cd_agency = '" & thisAgency & "' " & _sqlWhere & _
              "ORDER BY tblPayrollDetailsMain.sort_order"


        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")
    End Function

    Public Function browsePayrollFixTransactionDetailsDiff(ByVal thisHeaderID As String, ByVal thisEmployeeID As String) As DataTable

        Dim sql As String = ""

        'sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, cd_amount, cd_add_type, tblPayrollDetailsMain.sort_order, " & _
        '      "COALESCE((SELECT cd_amount FROM tbl_payroll_transaction_details WHERE tbl_payroll_transaction_details.transaction_employee_id = tblPayrollDetailsMain.transaction_employee_id AND " & _
        '      "tbl_payroll_transaction_details.cd_agency = tblPayrollDetailsMain.cd_agency AND tbl_payroll_transaction_details.payroll_item_type = 'GOVERNMENT' AND " & _
        '      "tbl_payroll_transaction_details.cd_type = 'SHARE' LIMIT 1),0.0) AS govShare FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
        '      "WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND (payroll_item_type='FIX DEDUCTION' OR cd_id = 'ECC') AND cd_amount>0 ORDER BY sort_order"

        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, SUM(cd_amount) AS cd_amount, cd_add_type, tblPayrollDetailsMain.sort_order, " & _
              "COALESCE((Select SUM(cd_amount) FROM tbl_payroll_transaction_details " & _
              "WHERE tbl_payroll_transaction_details.employee_id = tblPayrollDetailsMain.employee_id AND tbl_payroll_transaction_details.cd_agency = tblPayrollDetailsMain.cd_agency " & _
              "AND tbl_payroll_transaction_details.payroll_item_type = 'GOVERNMENT' AND tbl_payroll_transaction_details.cd_type = 'SHARE' AND " & _
              "tbl_payroll_transaction_details.transaction_header_id = tblPayrollDetailsMain.transaction_header_id " & _
              "GROUP BY cd_id LIMIT 1),0.0) AS govShare " & _
              "FROM tbl_payroll_transaction_details AS tblPayrollDetailsMain " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND employee_id = '" & thisEmployeeID & "' AND " & _
              "(payroll_item_type='FIX DEDUCTION' OR cd_id = 'ECC') AND cd_amount>0 " & _
              "GROUP BY cd_id " & _
              "ORDER BY sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")

    End Function

    Public Function browsePayrollTransactionDetailsOI(ByVal thisHeaderID As String, ByVal thisEmployeeID As String, ByVal thisCD As String) As DataTable

        Dim sql As String = ""

        sql = "SELECT transaction_details_id, tbl_payroll_transaction_details.transaction_header_id, transaction_employee_id, tbl_payroll_transaction_details.employee_id, payroll_item_type, cd_id, cd_type, " & _
              "cd_name, cd_amount , cd_add_type, tbl_payroll_transaction_details.sort_order,ot_hour,ot_minute, salary,leave_credit  FROM tbl_payroll_transaction_details " & _
              "INNER JOIN tbl_payroll_transaction_employees ON tbl_payroll_transaction_details.transaction_employee_id = tbl_payroll_transaction_employees.transaction_employees_id " & _
              " WHERE tbl_payroll_transaction_details.transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND payroll_item_type='" & thisCD & "' ORDER BY tbl_payroll_transaction_details.sort_order"

        'sql = "SELECT transaction_details_id, tbl_payroll_transaction_details.transaction_header_id, transaction_employee_id, tbl_payroll_transaction_details.employee_id, " & _
        '      "payroll_item_type, cd_id, cd_type, cd_name, cd_add_type, tbl_payroll_transaction_details.sort_order,ot_hour,ot_minute, salary,leave_credit,cd_amount, " & _
        '      "(cd_amount - COALESCE((SELECT COALESCE(cd_amount,0) FROM tbl_payroll_transaction_details AS tblWo WHERE tblWo.cd_id  = tbl_payroll_transaction_details.cd_id AND tblWo.cd_type = 'WITHOUT PAY' AND " & _
        '      "tblWo.transaction_employee_id = '" & thisEmployeeID & "'  LIMIT 1),0)) AS actualAmount " & _
        '      "FROM tbl_payroll_transaction_details " & _
        '      "INNER JOIN tbl_payroll_transaction_employees ON tbl_payroll_transaction_details.transaction_employee_id = tbl_payroll_transaction_employees.transaction_employees_id " & _
        '      "WHERE tbl_payroll_transaction_details.transaction_header_id = '" & thisHeaderID & "' AND transaction_employee_id = '" & thisEmployeeID & "' AND payroll_item_type='" & thisCD & "' ORDER BY tbl_payroll_transaction_details.sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")
    End Function

    Public Function browsePayrollTransactionDetailsCdId(ByVal thisHeaderID As String, ByVal thisCD As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT transaction_details_id, transaction_header_id, transaction_employee_id, employee_id, payroll_item_type, cd_id, cd_type, cd_name, cd_amount, cd_add_type, sort_order FROM tbl_payroll_transaction_details " & _
        " WHERE transaction_header_id = '" & thisHeaderID & "' AND cd_id='" & thisCD & "' ORDER BY sort_order"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_details")
    End Function

    Public Sub savePayrollTransactionDetails()
        If transactionDetailsId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "transaction_details_id,transaction_header_id,transaction_employee_id,employee_id,payroll_item_type,cd_id,cd_type,cd_name,cd_amount,cd_column_number,cd_add_type,cd_agency,sort_order"
                .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "INSERT")
                _transactionDetailsId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
                .ADDPARAM_CMD_String("transaction_details_id", _transactionDetailsId)
                .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
                .ADDPARAM_CMD_String("transaction_employee_id", _transactionEmployeeId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("payroll_item_type", _payrollItemType)
                .ADDPARAM_CMD_String("cd_id", _cdId)
                .ADDPARAM_CMD_String("cd_type", _cdType)
                .ADDPARAM_CMD_String("cd_name", _cdName)
                .ADDPARAM_CMD_String("cd_amount", _cdAmount)
                .ADDPARAM_CMD_String("cd_column_number", _cdColumnNumber)
                .ADDPARAM_CMD_String("cd_add_type", _cdAddType)
                .ADDPARAM_CMD_String("cd_agency", _cdAgency)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "cd_amount"
                .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "UPDATE", "transaction_details_id")
                .ADDPARAM_CMD_String("cd_amount", _cdAmount)
                .ADDPARAM_CMD_String("transaction_details_id", _transactionDetailsId)
                .executeUsingCommandFromSQL(True)
            End With

        End If
    End Sub

    Public Sub updatePayrollTransactionDetailsRemittance(thisRemittanceID As String)
        With _clsDB.dbUtility
            .fieldItems = "remittance_id"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "UPDATE", "transaction_details_id")
            .ADDPARAM_CMD_String("remittance_id", thisRemittanceID)
            .ADDPARAM_CMD_String("transaction_details_id", _transactionDetailsId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionDetailsCDAmount()
        With _clsDB.dbUtility
            .fieldItems = "cd_amount"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "UPDATE", "transaction_details_id")
            .ADDPARAM_CMD_String("cd_amount", _cdAmount)
            .ADDPARAM_CMD_String("transaction_details_id", _transactionDetailsId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionDetailsIsActive()
        With _clsDB.dbUtility
            .fieldItems = "is_active"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "UPDATE", "transaction_employee_id")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeeId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionDetailsMultipleIsActive()
        With _clsDB.dbUtility
            .fieldItems = "is_active"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "UPDATE", "transaction_header_id,employee_id", "AND")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionRemittance()

        With _clsDB.dbUtility
            .fieldItems = "remittance_id"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "UPDATE", "transaction_employee_id,cd_id", "AND")
            .ADDPARAM_CMD_String("remittance_id", _remittanceId)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeeId)
            .ADDPARAM_CMD_String("cd_id", _cdId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollTransactionRemittanceByRemId(ByVal _thisRemittanceId As String)

        With _clsDB.dbUtility
            .fieldItems = "remittance_id"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "UPDATE", "remittance_id")
            .ADDPARAM_CMD_String("remittance_id", _remittanceId)
            .ADDPARAM_CMD_String("remittance_id2", _thisRemittanceId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollTransactionDetailsUtil()

        With _clsDB.dbUtility
            .fieldItems = "transaction_header_id,prev_transaction_header_id,is_active"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_details", "UPDATE", "transaction_employee_id", )
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .ADDPARAM_CMD_String("prev_transaction_header_id", _prevTransactionHeaderId)
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("transaction_employee_id", _transactionEmployeeId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


    Public Sub getPayrollTransactionDetails(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_transaction_details WHERE transaction_details_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _transactionDetailsId = dt.Rows(0)("transaction_details_id").ToString
            _transactionHeaderId = dt.Rows(0)("transaction_header_id").ToString
            _transactionEmployeeId = dt.Rows(0)("transaction_employee_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _payrollItemType = dt.Rows(0)("payroll_item_type").ToString
            _cdId = dt.Rows(0)("cd_id").ToString
            _cdType = dt.Rows(0)("cd_type").ToString
            _cdName = dt.Rows(0)("cd_name").ToString
            _cdAmount = dt.Rows(0)("cd_amount").ToString
            _cdAddType = dt.Rows(0)("cd_add_type").ToString
            _sortOrder = dt.Rows(0)("sort_order").ToString
            _createDatetime = dt.Rows(0)("create_datetime").ToString
        Else
            initialize()
        End If
    End Sub
    Public Sub deletePayrollTransactionDetails(ByVal thisHeaderID As String, ByVal thisEmployee As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_details WHERE transaction_header_id='" & thisHeaderID & "' AND transaction_employee_id='" & thisEmployee & "'")
    End Sub

    Public Sub deletePayrollTransactionDetailsAll(ByVal thisHeaderID As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_details WHERE transaction_header_id='" & thisHeaderID & "'")
    End Sub

    Public Sub deletePayrollTransactionDetailsMultiple(ByVal thisHeaderID As String, ByVal thisEmployee As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_details WHERE transaction_header_id='" & thisHeaderID & "' AND employee_id='" & thisEmployee & "'")
    End Sub

    Public Sub deletePayrollTransactionDetailsWithoutPay(ByVal thisHeaderID As String, ByVal thisEmployee As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_details WHERE transaction_header_id='" & thisHeaderID & "' AND transaction_employee_id='" & thisEmployee & "' AND payroll_item_type='WITHOUT PAY'")
    End Sub
    Public Sub deletePayrollTransactionDetailsByItem(ByVal thisItem As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_details WHERE transaction_details_id='" & thisItem & "' LIMIT 1")
    End Sub

    Public Sub deletePayrollTransactionDetailsByPayrollItemType(ByVal thisHeaderID As String, ByVal thisEmployee As String, thisPayrollItemType As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_details WHERE transaction_header_id='" & thisHeaderID & "' AND transaction_employee_id='" & thisEmployee & "' AND payroll_item_type = '" & thisPayrollItemType & "'")
    End Sub

    Public Sub deletePayrollTransactionDetailsByCdType(ByVal thisHeaderID As String, ByVal thisEmployee As String, thisCdId As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_details WHERE transaction_header_id='" & thisHeaderID & "' AND transaction_employee_id='" & thisEmployee & "' AND cd_id='" & thisCdId & "'")
    End Sub

    Public Sub deletePayrollTransactionDetailsByPayrollAuto(ByVal thisHeaderID As String, ByVal thisEmployee As String, thisCdType As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_details WHERE transaction_header_id='" & thisHeaderID & "' AND transaction_employee_id='" & thisEmployee & "' AND cd_add_type = '" & thisCdType & "'")
    End Sub
   

End Class

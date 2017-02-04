<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>Task Management | Password Recovery</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.6 -->
  <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
  <!-- iCheck -->
  <link rel="stylesheet" href="plugins/iCheck/square/blue.css">

  <link rel="icon" href="fav2.png" type="image/png" sizes="16x16">

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    
    <style>
 h4 
        {
            font-size: 15px;
            margin-bottom: 0px;
        }
    </style>

</head>
<body class="hold-transition login-page">
              <div runat="server" class="alert alert-success alert-dismissible" id="SuccessBox" style="display:none; margin-bottom:0px;">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h4><i class="icon fa fa-check"></i>Verification code has been sent!</h4>
              </div>
              <div runat="server" class="alert alert-success alert-dismissible" id="PasswordChanged" style="display:none; margin-bottom:0px;">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h4><i class="icon fa fa-check"></i>Password Changed! Go back to Log In to continue.</h4>
              </div>
    <form id="form1" runat="server">
    <div class="login-box">
  <div class="login-logo" style="color:#f2f2f3">
    <h2><b>Task Management Software</b></h2>
  </div>

   <!-- /.login-logo -->
  <div class="login-box-body">
    <p class="login-box-msg">Enter Verification Code</p>

    
      <div class="form-group has-feedback">
        <!--input type="email" class="form-control" placeholder="Email"-->
          <asp:TextBox ID="TextBox1" runat="server" placeholder="Verification Code" 
              CssClass="form-control"></asp:TextBox>
        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
        <div class="small" id="error1" style="display:none" runat="server">
        Invalid code
        </div>
      </div>
      <div class="form-group has-feedback">
        <!--input type="password" class="form-control" placeholder="Password"-->
          <asp:TextBox ID="TextBox2" TextMode="Password" CssClass="form-control" 
              placeholder="New Password" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <!--input type="password" class="form-control" placeholder="Password"-->
          <asp:TextBox ID="TextBox3" TextMode="Password" CssClass="form-control" 
              placeholder="Re-enter New Password" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-log-in form-control-feedback"></span>
      </div>
    

  

    <a href="Login.aspx">Back to Log In</a>

     <div id="submitBtn" class="col-xs-4" style="float:right">
          <!--button type="submit" class="btn btn-primary btn-block btn-flat">Sign In</button-->
            <asp:Button ID="Button1" CssClass="btn btn-primary btn-block btn-flat" 
                runat="server" Text="Submit" onclick="Button1_Click"/>
        </div>
    </div>
  <!-- /.login-box-body -->
  </form>
      <!-- jQuery 2.2.3 -->
<script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="bootstrap/js/bootstrap.min.js"></script>
<!-- iCheck -->
<script src="plugins/iCheck/icheck.min.js"></script>

<script>
    $(document).ready(function () {
        $("#TextBox1").keyup(function () {
            $("#error1").remove();
            $("#TextBox1").css("border-color", "");
        });
    });
</script>

<script src="plugins/validation/dist/js/bootstrapValidator.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#form1').bootstrapValidator({
                fields: {
                    <%=TextBox1.UniqueID%>: {
                        validators: {
                            notEmpty: {
                                message: 'This field is required'
                            }
                        }
                    },
                <%=TextBox2.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'This field is required'
                        }
                    }
                },
                <%=TextBox3.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'This field is required'
                        },
                        identical: {
                        field: '<%=TextBox2.UniqueID%>',
                        message: 'The password and its confirm are not the same'
                    }
                    }
                }
              }
            });
        });
    </script>
</body>
</html>

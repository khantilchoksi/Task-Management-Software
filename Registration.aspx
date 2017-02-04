<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>Task Management | Registration Page</title>
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

  <style>
    .aloo, .aloo:hover 
    {
        float:right;
        color:White;
    }
    .aloo:hover
    {
        text-decoration:underline;
    }
  </style>

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
  <link rel="stylesheet" href="plugins/validation/dist/css/bootstrapValidator.min.css" />
</head>
<body class="hold-transition register-page">
    <form id="form1" runat="server">
   <div class="register-box">
  <div class="register-logo" style="color:#f2f2f3">
    <!--a href="index2.html"><b>Admin</b>LTE</a-->
    <b>Task Management Software</b>
  </div>

  <!--div class="register-box-body">
    <!--p class="login-box-msg">Register a new membership</p>

    </div-->
    
  <div class="register-box-body">
    <p class="login-box-msg">Register a new membership</p>

    <form action="../../index.html" method="post">
      <div class="form-group has-feedback">
        <!--input type="text" class="form-control" placeholder="First name"-->
        <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="First name" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-user form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <!--input type="text" class="form-control" placeholder="First name"-->
        <asp:TextBox ID="TextBox3" CssClass="form-control" placeholder="Last name" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-user form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <!--input type="email" class="form-control" placeholder="Email"-->
        <asp:TextBox ID="TextBox6" CssClass="form-control" placeholder="Mobile" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-phone form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <!--input type="email" class="form-control" placeholder="Email"-->
        <asp:TextBox ID="TextBox2" CssClass="form-control" placeholder="Email" 
              runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
        <div id="error1" style="display:none" runat="server">
        <small>Email already exists</small>
        </div>
      </div>
      <div class="form-group has-feedback">
        <!--input type="password" class="form-control" placeholder="Password"-->
        <asp:TextBox ID="TextBox4" CssClass="form-control" TextMode="Password" placeholder="Password" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <!--input type="password" class="form-control" placeholder="Retype password"-->
        <asp:TextBox ID="TextBox5" CssClass="form-control" TextMode="Password" placeholder="Re-enter Password" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-log-in form-control-feedback"></span>
      </div-->
      <!--div class="row">
        <!--div class="col-xs-8">
          <div class="checkbox icheck">
            <label>
              <input type="checkbox"> I agree to the <a href="#">terms</a>
            </label>
          </div>
        </div>
        <!-- /.col -->
              
        <!-- /.col -->
      <!--/div-->
    </div>
    
    </form>
    <%--<!--div class="social-auth-links text-center">
      <p>- OR -</p>
      <a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign up using
        Facebook</a>
      <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i> Sign up using
        Google+</a>
    </div>

    <a href="login.html" class="text-center">I already have a membership</a>
  </div>
  <!-- /.form-box -->--%>
</div>
<!-- /.register-box -->
<br />
<div class="col-xs-offset-1col-xs-4">

       <asp:Button ID="Button1" type="submit" CssClass="btn btn-primary btn-block btn-flat" 
                runat="server" Text="Register" onclick="Button1_Click" />
                <br />
          <!--button type="submit" class="btn btn-primary btn-block btn-flat">Register</button-->
        </div>
        <div>
        <a href="Login.aspx" class="text-center aloo"> Already a member? Sign in</a>
        </div>
</form>
<!-- jQuery 2.2.3 -->
<script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="bootstrap/js/bootstrap.min.js"></script>
<!-- iCheck -->
<script src="plugins/iCheck/icheck.min.js"></script>
<script>
    $(document).ready(function () {
        $("#TextBox2").keyup(function () {
            $("#error1").remove();
            $("#TextBox2").css("border-color", "");
        });
    });
</script>
<script>
    $(function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
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
                        },
                        emailAddress: {
                            message: 'Invalid Email Format'
                        }
                    }
                },
                <%=TextBox3.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'This field is required'
                        }
                    }
                },
                <%=TextBox4.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'This field is required'
                        }
                    }
                },
                <%=TextBox5.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'This field is required'
                        },
                        identical: {
                        field: '<%=TextBox4.UniqueID%>',
                        message: 'The password and its confirm are not the same'
                    }
                    }
                },
                <%=TextBox6.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'This field is required'
                        }
                    }
                }
            }
        });
    });
</script>
</body>
</html>

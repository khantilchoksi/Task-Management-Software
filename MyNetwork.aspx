<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyNetwork.aspx.cs" Inherits="MyNetwork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="plugins/validation/dist/css/bootstrapValidator.min.css" />
<title>My Network </title>
  <style>
    .users-list>li 
    {
        width:12%;
    }
    h4 
        {
            font-size: 15px;
            margin-bottom: 0px;
        }
    </style>
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div runat="server" class="alert alert-success alert-dismissible" id="SuccessBox" style="display:none; margin-bottom:0px;">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h4><i class="icon fa fa-check"></i>Invite Sent!</h4>
              </div>

 <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        <div runat="server" id="ptitle">
        My Network
        </div>
        <small><!--advanced tables--></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Network</li>
      </ol>
    </section>


    <section class="content">
      <div class="row">
        <div class="col-xs-12">

       <!-- /.box -->
          
          <!-- /.box -->



        </div>
        <!-- /.col -->
      </div>

        <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Add New Member</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <div class="form-horizontal">
            <!--form class="form-horizontal"-->
              <div class="box-body">

                <div class="form-group">
                  <label for="TextBox1" style="color:#444" class="col-sm-12 control-label">Member Email</label>
                  <div class="col-sm-6">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                <div id="error1" class="small" style="display:none" runat="server">
               User does not exist 
               
               </div>
               <div id="error2" class="small" style="display:none" runat="server">
               Request already sent 
               
               </div>
               <div id="error3" class="small" style="display:none" runat="server">
               Already in Network 
               
               </div>
               <div id="error4" class="small" style="display:none" runat="server">
               Request already received in notifications 
               
               </div>
               <div id="error5" class="small" style="display:none" runat="server">
               Cannot send request to self.
               
               </div>
                </div>

                <div>
                <asp:Button ID="Button1" runat="server" Text="Send Invite" 
                        CssClass="btn btn-primary" onclick="Button1_Click"></asp:Button>
               
                </div>

              </div>
              <!-- /.box-body -->
              </div>
              
              <!-- /.box-footer -->
            <!--/form-->
            </div>
            </div>
            
            
            <div class="box box-primary">
            <div class="box-header">
              <h3 class="box-title">Members</h3>

              <span style="float:right">
              
                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" onclick="LinkButton1_Click">Print Contacts</asp:LinkButton>
              </span>

            </div>
            <!-- /.box-header -->

            <div runat="server" id="NoMembers" class="box-body table-responsive" style="overflow-x:visible">
            </div>
            <!--div class="box-body" id="memberBox"></div-->
            <div class="box-body no-padding">
                  <ul runat="server" class="users-list clearfix" id="users_list"></ul>
                  <br />

                  <!-- /.users-list -->
                </div>
            <!-- /.box-body -->
          </div>
            
            
            </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<script>
    $(document).ready(function () {
        $("#ContentPlaceHolder1_TextBox1").keyup(function () {
            $("#ContentPlaceHolder1_error1").remove();
            $("#ContentPlaceHolder1_TextBox1").css("border-color", "");
        });
    });
</script>
<script>
    $(document).ready(function () {
        $("#ContentPlaceHolder1_TextBox1").keyup(function () {
            $("#ContentPlaceHolder1_error2").remove();
            $("#ContentPlaceHolder1_TextBox1").css("border-color", "");
        });
    });
</script>

<script>
    $(document).ready(function () {
        $("#ContentPlaceHolder1_TextBox1").keyup(function () {
            $("#ContentPlaceHolder1_error3").remove();
            $("#ContentPlaceHolder1_TextBox1").css("border-color", "");
        });
    });
</script>


<script>
    $(document).ready(function () {
        $("#ContentPlaceHolder1_TextBox1").keyup(function () {
            $("#ContentPlaceHolder1_error4").remove();
            $("#ContentPlaceHolder1_TextBox1").css("border-color", "");
        });
    });
</script>


<script>
    $(document).ready(function () {
        $("#ContentPlaceHolder1_TextBox1").keyup(function () {
            $("#ContentPlaceHolder1_error5").remove();
            $("#ContentPlaceHolder1_TextBox1").css("border-color", "");
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
                            },
                        emailAddress: {
                            message: 'Invalid Email Format'
                        }
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>


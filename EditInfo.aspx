<%@ Page Title="Edit Info" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditInfo.aspx.cs" Inherits="EditInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="plugins/validation/dist/css/bootstrapValidator.min.css" />
    <style>
        h4 
        {
            font-size: 15px;
            margin-bottom: 0px;
        }
        .resizenone 
        {
            resize:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div runat="server" class="alert alert-success alert-dismissible" id="SuccessBox" style="display:none; margin-bottom:0px;">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h4><i class="icon fa fa-check"></i> Info Updated!</h4>
              </div>
<div runat="server" class="alert alert-success alert-dismissible" id="Success1" style="display:none; margin-bottom:0px;">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h4><i class="icon fa fa-check"></i> Password Changed!</h4>
              </div>
<section class="content-header">
      <h1>
        Edit Profile
        <!--small>Preview</small-->
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Edit Profile</li>
      </ol>
    </section>

    <section class="content">
      <div class="row">
        <%--<!-- left column -->
        <div class="col-md-6">
          <!-- general form elements -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Quick Example</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <form role="form">
              <div class="box-body">
                <div class="form-group">
                  <label for="exampleInputEmail1">Email address</label>
                  <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">
                </div>
                <div class="form-group">
                  <label for="exampleInputPassword1">Password</label>
                  <input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password">
                </div>
                <div class="form-group">
                  <label for="exampleInputFile">File input</label>
                  <input type="file" id="exampleInputFile">

                  <p class="help-block">Example block-level help text here.</p>
                </div>
                <div class="checkbox">
                  <label>
                    <input type="checkbox"> Check me out
                  </label>
                </div>
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
                <button type="submit" class="btn btn-primary">Submit</button>
              </div>
            </form>
          </div>
          <!-- /.box -->

          <!-- Form Element sizes -->
          <div class="box box-success">
            <div class="box-header with-border">
              <h3 class="box-title">Different Height</h3>
            </div>
            <div class="box-body">
              <input class="form-control input-lg" type="text" placeholder=".input-lg">
              <br>
              <input class="form-control" type="text" placeholder="Default input">
              <br>
              <input class="form-control input-sm" type="text" placeholder=".input-sm">
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

          <div class="box box-danger">
            <div class="box-header with-border">
              <h3 class="box-title">Different Width</h3>
            </div>
            <div class="box-body">
              <div class="row">
                <div class="col-xs-3">
                  <input type="text" class="form-control" placeholder=".col-xs-3">
                </div>
                <div class="col-xs-4">
                  <input type="text" class="form-control" placeholder=".col-xs-4">
                </div>
                <div class="col-xs-5">
                  <input type="text" class="form-control" placeholder=".col-xs-5">
                </div>
              </div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

          <!-- Input addon -->
          <div class="box box-info">
            <div class="box-header with-border">
              <h3 class="box-title">Input Addon</h3>
            </div>
            <div class="box-body">
              <div class="input-group">
                <span class="input-group-addon">@</span>
                <input type="text" class="form-control" placeholder="Username">
              </div>
              <br>

              <div class="input-group">
                <input type="text" class="form-control">
                <span class="input-group-addon">.00</span>
              </div>
              <br>

              <div class="input-group">
                <span class="input-group-addon">$</span>
                <input type="text" class="form-control">
                <span class="input-group-addon">.00</span>
              </div>

              <h4>With icons</h4>

              <div class="input-group">
                <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                <input type="email" class="form-control" placeholder="Email">
              </div>
              <br>

              <div class="input-group">
                <input type="text" class="form-control">
                <span class="input-group-addon"><i class="fa fa-check"></i></span>
              </div>
              <br>

              <div class="input-group">
                <span class="input-group-addon"><i class="fa fa-dollar"></i></span>
                <input type="text" class="form-control">
                <span class="input-group-addon"><i class="fa fa-ambulance"></i></span>
              </div>

              <h4>With checkbox and radio inputs</h4>

              <div class="row">
                <div class="col-lg-6">
                  <div class="input-group">
                        <span class="input-group-addon">
                          <input type="checkbox">
                        </span>
                    <input type="text" class="form-control">
                  </div>
                  <!-- /input-group -->
                </div>
                <!-- /.col-lg-6 -->
                <div class="col-lg-6">
                  <div class="input-group">
                        <span class="input-group-addon">
                          <input type="radio">
                        </span>
                    <input type="text" class="form-control">
                  </div>
                  <!-- /input-group -->
                </div>
                <!-- /.col-lg-6 -->
              </div>
              <!-- /.row -->

              <h4>With buttons</h4>

              <p class="margin">Large: <code>.input-group.input-group-lg</code></p>

              <div class="input-group input-group-lg">
                <div class="input-group-btn">
                  <button type="button" class="btn btn-warning dropdown-toggle" data-toggle="dropdown">Action
                    <span class="fa fa-caret-down"></span></button>
                  <ul class="dropdown-menu">
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li class="divider"></li>
                    <li><a href="#">Separated link</a></li>
                  </ul>
                </div>
                <!-- /btn-group -->
                <input type="text" class="form-control">
              </div>
              <!-- /input-group -->
              <p class="margin">Normal</p>

              <div class="input-group">
                <div class="input-group-btn">
                  <button type="button" class="btn btn-danger">Action</button>
                </div>
                <!-- /btn-group -->
                <input type="text" class="form-control">
              </div>
              <!-- /input-group -->
              <p class="margin">Small <code>.input-group.input-group-sm</code></p>

              <div class="input-group input-group-sm">
                <input type="text" class="form-control">
                    <span class="input-group-btn">
                      <button type="button" class="btn btn-info btn-flat">Go!</button>
                    </span>
              </div>
              <!-- /input-group -->
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

        </div>
        <!--/.col (left) -->--%>
        <!-- right column -->
        <div class="col-md-12">
          <!-- Horizontal Form -->
          <div class="box box-info">
            <div class="box-header with-border">
              <h3 class="box-title">Personal Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <div class="form-horizontal">
            <!--form class="form-horizontal"-->
              <div class="box-body">
              <div class="col-md-6">
                <div class="form-group">
                  <label for="TextBox11" class="col-sm-12 control-label">First Name</label>
                  <div class="col-sm-9">
                    <!--input type="email" class="form-control" id="inputEmail3" placeholder="Email"-->
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                  </div>
                </div>

                <div class="form-group">
                  <label for="TextBox2" class="col-sm-12 control-label">Last Name</label>
                  <div class="col-sm-9">
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                  </div>
                </div>

                

                <div class="form-group">
                  <label for="TextBox6" class="col-sm-12 control-label">Description</label>
                  <div class="col-sm-9">
                    <asp:TextBox TextMode="MultiLine" Rows="3" ID="TextBox6" runat="server" CssClass="form-control resizenone" placeholder="About Me"></asp:TextBox>
                  </div>
                </div>
                   
                
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label for="TextBox3" class="col-sm-12 control-label">Mobile</label>
                  <div class="col-sm-9">
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Mobile"></asp:TextBox>
                  </div>
                </div><br />
                <div class="form-group">
                   <label class="col-sm-12 control-label">Upload/Change Profile Picture</label>
                   <div class="col-sm-9">
                        <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload><br />
                        <img runat="server" id="image_upload_preview" class="img-circle" style="width:100px; height:100px;" src="" alt="Image Preview" title="Preview" />
                   </div>
                </div>
              </div>
              </div>
              <!-- /.box-body -->
              </div>
              <div class="box-footer">
                <!--button type="submit" class="btn btn-default">Cancel</button-->
                <a href="Dashboard.aspx" class="btn btn-default">Cancel</a>
                <!--button type="submit" class="btn btn-info pull-right">Sign in</button-->
                <asp:Button ID="Button1" runat="server" Text="Save Changes" onclick="Button1_Click" CssClass="btn btn-primary pull-right"></asp:Button>
              </div>
              <!-- /.box-footer -->
            <!--/form-->
            </div>



            <div class="box box-info">
            <div class="box-header with-border">
              <h3 class="box-title">Change Password</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <div class="form-horizontal">
            <!--form class="form-horizontal"-->
              <div class="box-body">

                <div class="form-group">
                  <label for="TextBox4" class="col-sm-12 control-label">New Password</label>
                  <div class="col-sm-6">
                    <asp:TextBox ID="TextBox4" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
                  </div>
                </div>

                <div class="form-group">
                  <label for="TextBox5" class="col-sm-12 control-label">Confirm Password</label>
                  <div class="col-sm-6">
                    <asp:TextBox ID="TextBox5" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm Password"></asp:TextBox>
                  </div>
                </div>

              </div>
              <!-- /.box-body -->
              </div>
              <div class="box-footer">
                <!--button type="submit" class="btn btn-default">Cancel</button-->
                <a href="Dashboard.aspx" class="btn btn-default">Cancel</a>
                <!--button type="submit" class="btn btn-info pull-right">Sign in</button-->
                <asp:Button ID="Button2" runat="server" Text="Change Password" onclick="Button2_Click" CssClass="btn btn-primary pull-right"></asp:Button>
              </div>
              <!-- /.box-footer -->
            <!--/form-->
            </div>

          </div>
          <!-- /.box -->
          <!-- general form elements disabled -->
          <%--<div class="box box-warning">
            <div class="box-header with-border">
              <h3 class="box-title">General Elements</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <form role="form">
                <!-- text input -->
                <div class="form-group">
                  <label>Text</label>
                  <input type="text" class="form-control" placeholder="Enter ...">
                </div>
                <div class="form-group">
                  <label>Text Disabled</label>
                  <input type="text" class="form-control" placeholder="Enter ..." disabled>
                </div>

                <!-- textarea -->
                <div class="form-group">
                  <label>Textarea</label>
                  <textarea class="form-control" rows="3" placeholder="Enter ..."></textarea>
                </div>
                <div class="form-group">
                  <label>Textarea Disabled</label>
                  <textarea class="form-control" rows="3" placeholder="Enter ..." disabled></textarea>
                </div>

                <!-- input states -->
                <div class="form-group has-success">
                  <label class="control-label" for="inputSuccess"><i class="fa fa-check"></i> Input with success</label>
                  <input type="text" class="form-control" id="inputSuccess" placeholder="Enter ...">
                  <span class="help-block">Help block with success</span>
                </div>
                <div class="form-group has-warning">
                  <label class="control-label" for="inputWarning"><i class="fa fa-bell-o"></i> Input with
                    warning</label>
                  <input type="text" class="form-control" id="inputWarning" placeholder="Enter ...">
                  <span class="help-block">Help block with warning</span>
                </div>
                <div class="form-group has-error">
                  <label class="control-label" for="inputError"><i class="fa fa-times-circle-o"></i> Input with
                    error</label>
                  <input type="text" class="form-control" id="inputError" placeholder="Enter ...">
                  <span class="help-block">Help block with error</span>
                </div>

                <!-- checkbox -->
                <div class="form-group">
                  <div class="checkbox">
                    <label>
                      <input type="checkbox">
                      Checkbox 1
                    </label>
                  </div>

                  <div class="checkbox">
                    <label>
                      <input type="checkbox">
                      Checkbox 2
                    </label>
                  </div>

                  <div class="checkbox">
                    <label>
                      <input type="checkbox" disabled>
                      Checkbox disabled
                    </label>
                  </div>
                </div>

                <!-- radio -->
                <div class="form-group">
                  <div class="radio">
                    <label>
                      <input type="radio" name="optionsRadios" id="optionsRadios1" value="option1" checked>
                      Option one is this and that&mdash;be sure to include why it's great
                    </label>
                  </div>
                  <div class="radio">
                    <label>
                      <input type="radio" name="optionsRadios" id="optionsRadios2" value="option2">
                      Option two can be something else and selecting it will deselect option one
                    </label>
                  </div>
                  <div class="radio">
                    <label>
                      <input type="radio" name="optionsRadios" id="optionsRadios3" value="option3" disabled>
                      Option three is disabled
                    </label>
                  </div>
                </div>

                <!-- select -->
                <div class="form-group">
                  <label>Select</label>
                  <select class="form-control">
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>
                </div>
                <div class="form-group">
                  <label>Select Disabled</label>
                  <select class="form-control" disabled>
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>
                </div>

                <!-- Select multiple-->
                <div class="form-group">
                  <label>Select Multiple</label>
                  <select multiple class="form-control">
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>
                </div>
                <div class="form-group">
                  <label>Select Multiple Disabled</label>
                  <select multiple class="form-control" disabled>
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>
                </div>

              </form>
            </div>
            <!-- /.box-body -->
          </div>--%>
          <!-- /.box -->
        </div>
        <!--/.col (right) -->
      
      <!-- /.row -->
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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
                            }
                        }
                    },
                    <%=TextBox5.UniqueID%>: {
                        validators: {
                            identical: {
                               field: '<%=TextBox4.UniqueID%>',
                               message: 'The password and its confirm are not the same'
                            }
                        }
                    },
                    <%=TextBox4.UniqueID%>: {
                        validators: {
                            identical: {
                               field: '<%=TextBox5.UniqueID%>',
                               message: 'The password and its confirm are not the same'
                            }
                        }
                    }
                }
            });
        });
    </script>
    <script>
        $('#ContentPlaceHolder1_Button2').click(function () {
            if ($.trim($('#ContentPlaceHolder1_TextBox4').val()) == '') {
                alert('Password cannot be empty');
                return false;
            }
            else {
                if ($.trim($('#ContentPlaceHolder1_TextBox4').val()) != ($.trim($('#ContentPlaceHolder1_TextBox5').val()))) {
                    alert('Passwords don\'t match');
                    return false;
                }
            }
        });
    </script>
    <script>
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#ContentPlaceHolder1_image_upload_preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        $("#ContentPlaceHolder1_FileUpload1").change(function () {
            readURL(this);
        });
    </script>
</asp:Content>


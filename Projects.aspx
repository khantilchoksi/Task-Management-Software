<%@ Page Title="Projects" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Projects.aspx.cs" Inherits="Projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- DataTables -->
    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css">
    <style>
        h4 
        {
            font-size: 15px;
            margin-bottom: 0px;
        }
        .btn-info, .btn-success, .btn-danger, .btn-warning
        {
            width:78px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <div runat="server" id="AccessBox" class="alert alert-danger alert-dismissible" style="display:none; margin-bottom: 0px;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4><i class="icon fa fa-ban"></i>Access Denied!</h4>
                </div>
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        My Projects
        <!--small>Control panel</small-->
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Projects</li>
      </ol>
    </section>

    <section class="content">
      <div class="row">
      <div class="col-xs-12">  
           <a href="AddProject.aspx" class="btn btn-primary"><i class="fa fa-plus"></i> Add New</a>
      </div>
      </div><br />
      <div class="row">
         <div class="col-md-12">
          <!-- Custom Tabs -->
          <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
              <li class="active"><a href="#tab_1" data-toggle="tab"><i class="fa fa-play-circle"></i> Active</a></li>
              <li><a href="#tab_2" data-toggle="tab"><i class="fa fa-pause"></i> Inactive</a></li>
              <li><a href="#tab_3" data-toggle="tab"><i class="fa fa-stop"></i> Completed</a></li>
              <!--li class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                  Actions <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                  <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Action</a></li>
                  <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Another action</a></li>
                  <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Something else here</a></li>
                  <li role="presentation" class="divider"></li>
                  <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Separated link</a></li>
                </ul>
              </li-->
              <!--li class="pull-right"><a href="#" class="text-muted"><i class="fa fa-gear"></i></a></li-->
              <li class="pull-right">
                <!--a href="AddProject.aspx" class="btn btn-primary"><i class="fa fa-plus"></i> Add New</a-->
              </li>
            </ul>
            <div class="tab-content">
              <div class="tab-pane active" id="tab_1">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="project_id" DataSourceID="SqlDataSource1" CssClass="table table-striped table-bordered" OnRowCreated="GridView1_OnRowCreated" OnRowDataBound="OnRowDataBound">
                  <Columns>
                      <asp:BoundField DataField="project_id" HeaderText="project_id" 
                          InsertVisible="False" ReadOnly="True" SortExpression="Project Id" />
                      <asp:BoundField DataField="project_title" HeaderText="Project Title" 
                          SortExpression="project_title" />
                      <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="view" runat="server" OnClick="viewProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-info"><i class="fa fa-search"></i> View</asp:LinkButton>
                            <asp:LinkButton ID="edit" runat="server" OnClick="editProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-success"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                            <asp:LinkButton ID="delete" runat="server" OnClick="deleteProject" OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CommandName="" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-danger"><i class="fa fa-trash-o"></i> Delete</asp:LinkButton>
                            <asp:LinkButton ID="tasks" runat="server" OnClick="showTasks" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-warning"><i class="fa fa-list"></i> Tasks</asp:LinkButton>
                        </ItemTemplate>
                      </asp:TemplateField>  
                  </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    
                    
                    SelectCommand="select project_id, project_title from projects where project_id in (select distinct(project_id) from project_network where member_id = @user) and deleted_at is null and project_status = 1 order by project_id">
                        <SelectParameters>
                            <asp:SessionParameter
                                Name="user"
                                SessionField="user"
                                DefaultValue="0" />
                        </SelectParameters>
                </asp:SqlDataSource>
                <div runat="server" id="tab1_msg"></div>
              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_2">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="project_id" DataSourceID="SqlDataSource2" CssClass="table table-striped table-bordered" OnRowCreated="GridView1_OnRowCreated" OnRowDataBound="OnRowDataBound">
                  <Columns>
                      <asp:BoundField DataField="project_id" HeaderText="project_id" 
                          InsertVisible="False" ReadOnly="True" SortExpression="Project Id" />
                      <asp:BoundField DataField="project_title" HeaderText="Project Title" 
                          SortExpression="project_title" />
                      <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="view" runat="server" OnClick="viewProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-info"><i class="fa fa-search"></i> View</asp:LinkButton>
                            <asp:LinkButton ID="edit" runat="server" OnClick="editProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-success"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                            <asp:LinkButton ID="delete" runat="server" OnClick="deleteProject" OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CommandName="" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-danger"><i class="fa fa-trash-o"></i> Delete</asp:LinkButton>
                            <asp:LinkButton ID="tasks" runat="server" OnClick="showTasks" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-warning"><i class="fa fa-list"></i> Tasks</asp:LinkButton>
                        </ItemTemplate>
                      </asp:TemplateField>  
                  </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    
                    
                    SelectCommand="select project_id, project_title from projects where project_id in (select distinct(project_id) from project_network where member_id = @user) and deleted_at is null and project_status = 3 order by project_id">
                        <SelectParameters>
                            <asp:SessionParameter
                                Name="user"
                                SessionField="user"
                                DefaultValue="0" />
                        </SelectParameters>
                </asp:SqlDataSource>
                <div runat="server" id="tab2_msg"></div>
              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_3">
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="project_id" DataSourceID="SqlDataSource3" CssClass="table table-striped table-bordered" OnRowCreated="GridView1_OnRowCreated" OnRowDataBound="OnRowDataBound">
                  <Columns>
                      <asp:BoundField DataField="project_id" HeaderText="project_id" 
                          InsertVisible="False" ReadOnly="True" SortExpression="Project Id" />
                      <asp:BoundField DataField="project_title" HeaderText="Project Title" 
                          SortExpression="project_title" />
                      <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="view" runat="server" OnClick="viewProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-info"><i class="fa fa-search"></i> View</asp:LinkButton>
                            <asp:LinkButton ID="edit" runat="server" OnClick="editProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-success"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                            <asp:LinkButton ID="delete" runat="server" OnClick="deleteProject" OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CommandName="" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-danger"><i class="fa fa-trash-o"></i> Delete</asp:LinkButton>
                            <asp:LinkButton ID="tasks" runat="server" OnClick="showTasks" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-warning"><i class="fa fa-list"></i> Tasks</asp:LinkButton>
                        </ItemTemplate>
                      </asp:TemplateField>  
                  </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    
                    
                    SelectCommand="select project_id, project_title from projects where project_id in (select distinct(project_id) from project_network where member_id = @user) and deleted_at is null and project_status = 2 order by project_id">
                        <SelectParameters>
                            <asp:SessionParameter
                                Name="user"
                                SessionField="user"
                                DefaultValue="0" />
                        </SelectParameters>
                </asp:SqlDataSource>
                <div runat="server" id="tab3_msg"></div>
              </div>
              <!-- /.tab-pane -->
            </div>
            <!-- /.tab-content -->
          </div>
          <!-- nav-tabs-custom -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <!-- DataTables -->
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>
    <!-- page script -->
    <script>
        $(function () {
            //$("#GridView1").DataTable();
            // Using aoColumnDefs
            $('#ContentPlaceHolder1_GridView1').dataTable({
                "aoColumnDefs": [
                    { "bSearchable": false, "aTargets": [1] }
                    ],
                "order": []
            });
            $('#ContentPlaceHolder1_GridView2').dataTable({
                "aoColumnDefs": [
                    { "bSearchable": false, "aTargets": [1] }
                    ],
                "order": []
            }); 
            $('#ContentPlaceHolder1_GridView3').dataTable({
                "aoColumnDefs": [
                    { "bSearchable": false, "aTargets": [1] }
                    ],
                "order": []
            });
            $('#GridVidew11').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": false
            });
        });
    </script>
</asp:Content>


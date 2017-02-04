<%@ Page Title="Project Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProjectDetails.aspx.cs" Inherits="ProjectDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
    .users-list>li 
    {
        width:12%;
    }
    .in_visible 
    {
        float:right;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        <div runat="server" id="ptitle">
        My Projects 
        </div>
        <small><!--advanced tables--></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="Projects.aspx">Projects</a></li>
        <li class="active">Project Details</li>
      </ol>
    </section>

    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          
          <!-- /.box -->
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Description</h3>
            </div>
            <!-- /.box-header -->
            <div runat="server" class="box-body" id="descBody"></div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

          <!-- /.box -->
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">General</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <asp:DetailsView ID="DetailsView1" runat="server" OnDataBound="DetailsView1_DataBound" AutoGenerateRows="False" DataSourceID="SqlDataSource1" CssClass="table table-striped table-bordered">
                    <Fields>
                        <asp:BoundField DataField="Admin" HeaderText="Admin" 
                            SortExpression="Admin" ReadOnly="True" />
                        <asp:BoundField DataField="project_status" HeaderText="Status" 
                            SortExpression="project_status" />
                        <asp:BoundField DataField="created_at" HeaderText="Created At" 
                            SortExpression="created_at" />
                        <asp:BoundField DataField="updated_at" HeaderText="Last Updated" 
                            SortExpression="updated_at" />
                    </Fields>
                </asp:DetailsView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    
                    SelectCommand="select u.first_name+' '+u.last_name as Admin, p.project_status, p.created_at, p.updated_at from users u, projects p where p.project_id = @project and u.user_id = p.created_by">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="project" SessionField="pid" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

          <!-- /.box -->
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Members</h3>
              <a runat="server" id="priv" href="EditProjectPriveleges.aspx" class="btn btn-primary" style="display:none; float:right;">Edit Priveleges</a>
            </div>
            <!-- /.box-header -->
            <!--div class="box-body" id="memberBox"></div-->
            <div class="box-body no-padding">
                  <ul runat="server" class="users-list clearfix" id="users_list"></ul>
                  <!-- /.users-list -->
                </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->



        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>


import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { ListsComponent } from './components/lists/lists.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MessagesComponent } from './components/messages/messages.component';
import { MemberCardComponent } from './components/members/member-card/member-card.component';
import { MemberDetailComponent } from './components/members/member-detail/member-detail.component';
import { MemberEditComponent } from './components/members/member-edit/member-edit.component';
import { PhotoEditorComponent } from './components/members/photo-editor/photo-editor.component';
import { TimeagoModule } from 'ngx-timeago';

import { UserService } from './services/user.service';
import { AuthService } from './services/auth.service';
import { ErrorInterceptorProvider } from './interceptors/error.interceptor';
import { MemberDetailResolver } from './resolvers/member-detail.resolver';
import { MemberListResolver } from './resolvers/member-list.resolver';
import { MemberEditResolver } from './resolvers/member-edit.resolver';

import { appRoutes } from './app.routing';
import { PreventUnsavedChangesGuard } from './guards/prevent-unsaved-changes.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    ListsComponent,
    MemberListComponent,
    MessagesComponent,
    MemberCardComponent,
    MemberDetailComponent,
    MemberEditComponent,
    PhotoEditorComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/Auth'],
      },
    }),
    TabsModule.forRoot(),
    NgxGalleryModule,
    FileUploadModule,
    BsDatepickerModule.forRoot(),
    TimeagoModule.forRoot(),
  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    UserService,
    MemberDetailResolver,
    MemberListResolver,
    MemberEditResolver,
    PreventUnsavedChangesGuard,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function tokenGetter() {
  return localStorage.getItem('token');
}

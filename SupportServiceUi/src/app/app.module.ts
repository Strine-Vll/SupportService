import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { UserService } from './services/user.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { DatePipe } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { GroupComponent } from './group/group.component';
import { RequestComponent } from './request/request.component';
import { CreateRequestComponent } from './create-request/create-request.component';
import { CreateGroupComponent } from './create-group/create-group.component';
import { ModalComponent } from './modal/modal.component';
import { CreateRequestModalComponent } from './create-request-modal/create-request-modal.component';
import { CommentsSectionComponent } from './comments-section/comments-section.component';
import { EditRequestModalComponent } from './edit-request-modal/edit-request-modal.component';
import { PromptModalComponent } from './prompt-modal/prompt-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    UserComponent,
    HomeComponent,
    HeaderComponent,
    GroupComponent,
    RequestComponent,
    CreateRequestComponent,
    CreateGroupComponent,
    ModalComponent,
    CreateRequestModalComponent,
    CommentsSectionComponent,
    EditRequestModalComponent,
    PromptModalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true,
      progressAnimation: 'increasing',
      enableHtml: true,
      tapToDismiss: true
    }),
    FormsModule,
    NgbModule,
  ],
  providers: [
    DatePipe,
    UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }

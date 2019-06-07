import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

import { SeasonClient, WorkoutClient, JudgingClient } from './brorep-api';

import { WorkoutListComponent } from './components/shared/workoutlist/workoutlist.component';

import { SubmitComponent } from './components/submit/submit.component';
import { LoadVideoComponent } from './components/submit/loadvideo/loadvideo.component';
import { VideoEditComponent } from './components/submit/videoedit/videoedit.component';
import { PreviewComponent } from './components/submit/preview/preview.component';

import { VideoComponent } from './components/video/video.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SubmitComponent,
    WorkoutListComponent,
    LoadVideoComponent,
    VideoEditComponent,
    PreviewComponent,
    VideoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule, ReactiveFormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'submit', component: SubmitComponent, canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [SeasonClient, WorkoutClient, JudgingClient,
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

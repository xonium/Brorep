import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

@Component({
    selector: 'app-workout',
    templateUrl: './workout.component.html',
    styleUrls: ['./workout.component.scss']
})
export class WorkoutComponent implements OnInit {

    loadThisVideo: string;
    loadVideoForm: FormGroup;
    seekerLocation: number;
    videoReady: boolean;

    constructor(private fb: FormBuilder) {
        this.loadVideoForm = this.fb.group({
            videoUrl: new FormControl('')
          });
    }

    ngOnInit(): void { }

    onLoadVideoFormSubmit() {
        this.loadThisVideo = this.loadVideoForm.get('videoUrl').value;
    }

    onVideoReady(event) {
        this.videoReady = true;
    }

    onVideoError(error) {
        console.log(error);
    }

    onSetSeekerLocation(value) {
        console.log('SEEKERLOCATION', value);
    }

    onVideoProgress(progress) {
        console.log(progress);
        this.seekerLocation = progress.played;
    }
}

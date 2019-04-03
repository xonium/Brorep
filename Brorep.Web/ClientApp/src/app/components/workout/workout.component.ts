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
    setSeekerLocation: number;
    videoReady: boolean;
    isVideoPlaying: boolean;
    videoShouldPlay: boolean;

    constructor(private fb: FormBuilder) {
        this.loadVideoForm = this.fb.group({
            videoUrl: new FormControl('')
          });
    }

    ngOnInit(): void { }

    onLoadVideoFormSubmit() {
        this.loadThisVideo = this.loadVideoForm.get('videoUrl').value;
    }

    onVideoPauseClick() {
        this.videoShouldPlay = false;
    }

    onVideoPlayClick() {
        this.videoShouldPlay = true;
    }

    onVideoReady(event) {
        this.videoReady = true;
    }

    onVideoError(error) {
        console.log(error);
    }

    onSetSeekerLocation(event) {
        const target = event.target || event.srcElement || event.currentTarget;
        const value = target.value;
        this.setSeekerLocation = value;
    }

    onVideoProgress(progress) {
        this.seekerLocation = progress.played;
    }

    onVideoPlay(isPlaying) {
        this.isVideoPlaying = isPlaying;
    }

    onVideoPause(isPausing) {
        this.isVideoPlaying = !isPausing;
    }
}

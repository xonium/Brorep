import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

interface RecordedRep {
    startTime: number;
    stopTime: number;
    length: number;
}

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
    isRecording: boolean;
    startRecord: number;
    stopRecord: number;
    recordedReps: RecordedRep[];

    constructor(private fb: FormBuilder) {
        this.loadVideoForm = this.fb.group({
            videoUrl: new FormControl('')
          });

          this.recordedReps = [];
          this.isRecording = false;
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
        if (this.isRecording) { return; }

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

    onVideoRecordClick() {
        this.isRecording = !this.isRecording;

        if (this.isRecording) {
            this.startRecord = this.seekerLocation;
        } else {
            this.stopRecord = this.seekerLocation;

            this.recordedReps.push(
            {
                startTime: this.startRecord * 100,
                stopTime: this.stopRecord * 100,
                length: (this.stopRecord - this.startRecord) * 100
            });

            console.log(this.recordedReps);
        }
    }
}

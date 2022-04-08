﻿using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController Instance;
    private CinemachineConfiner2D _confiner;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        _confiner = gameObject.GetComponent<CinemachineConfiner2D>();
    }

    public void UpdateConfiner(Collider2D boundingShape) {
        if (_confiner) {
            _confiner.m_BoundingShape2D = boundingShape;
        }

    }
    
}